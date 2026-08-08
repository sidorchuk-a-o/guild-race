using System.Collections.Generic;
using System.Linq;
using AD.Game.GuildLevels;
using AD.Game.Inventory;
using AD.Management;
using AD.Save;
using AD.ToolsCollection;
using Newtonsoft.Json;
using R3;

namespace AD.Game.Guild
{
    public class GuildState : ServiceState<GuildSM>
    {
        private readonly ReactiveProperty<string> guildName = new();
        private readonly ReactiveProperty<string> playerName = new();

        private readonly ReactiveProperty<int> maxCharactersCount = new();
        private readonly ReactiveProperty<float> requestTimePercent = new(1);

        private readonly CharactersCollection characters = new(null);
        private readonly GuildRanksCollection guildRanks = new(null);
        private readonly GuildBankTabsCollection bankTabs = new(null);

        private readonly GuildLevelContext levelContext = new();

        private readonly GuildConfig config;
        private readonly IGuildLevelsService guildLevelsService;
        private readonly IInventoryService inventoryService;

        public override string SaveKey => "guild";
        public override SaveSource SaveSource => SaveSource.App;

        public bool IsExists => guildName.IsValid();
        public ReadOnlyReactiveProperty<string> GuildName => guildName;
        public ReadOnlyReactiveProperty<string> PlayerName => playerName;

        public ReadOnlyReactiveProperty<int> MaxCharactersCount => maxCharactersCount;
        public ReadOnlyReactiveProperty<float> RequestTimePercent => requestTimePercent;

        public ICharactersCollection Characters => characters;
        public IGuildRanksCollection GuildRanks => guildRanks;
        public IGuildBankTabsCollection BankTabs => bankTabs;
        public EmblemInfo Emblem { get; private set; }

        public GuildState(GuildConfig config, IGuildLevelsService guildLevelsService, IInventoryService inventoryService)
        {
            this.config = config;
            this.guildLevelsService = guildLevelsService;
            this.inventoryService = inventoryService;
        }

        public override void Init()
        {
            base.Init();

            guildLevelsService.RegisterContext(levelContext);

            levelContext.BankRowCount.Subscribe(UpgradeBankRowCountCallback);
            levelContext.CharactersCount.Subscribe(UpgradeCharactersCountCallback);
            levelContext.RequestTimePercent.Subscribe(x => requestTimePercent.Value = x);
        }

        public void CreateOrUpdateGuild(GuildEM guildEM)
        {
            guildName.Value = guildEM.GuildName;
            playerName.Value = guildEM.PlayerName;

            SaveEmblem(guildEM.Emblem);

            MarkAsDirty(true);
        }

        // == Emblem ==

        public EmblemEM CreateEmblemEM()
        {
            return new(Emblem);
        }

        public void SaveEmblem(EmblemEM emblemEM)
        {
            Emblem.SetSymbol(emblemEM.Symbol);
            Emblem.SetSymbolColor(emblemEM.SymbolColor);
            Emblem.SetBackground(emblemEM.Background);
            Emblem.SetBackgroundColors(emblemEM.BackgroundColors);

            MarkAsDirty(true);
        }

        // == Characters ==

        public void AddCharacter(CharacterInfo info)
        {
            if (characters.Contains(info))
            {
                return;
            }

            characters.Add(info);

            MarkAsDirty(true);
        }

        public int RemoveCharacter(string characterId)
        {
            var index = characters.FindIndex(x => x.Id == characterId);

            characters.RemoveAt(index);

            MarkAsDirty(true);

            return index;
        }

        public void UpdateOrderByRank(CharacterInfo character)
        {
            var skipIndex = false;
            var newIndex = -1;
            var prevIndex = characters.IndexOf(character);

            var rankId = character.GuildRankId.CurrentValue;
            var rankIndex = guildRanks.IndexOf(rankId);

            for (var i = 0; i < characters.Count; i++)
            {
                if (characters[i] == character)
                {
                    skipIndex = true;
                    continue;
                }

                var cRankId = characters[i].GuildRankId.CurrentValue;
                var cRankIndex = guildRanks.IndexOf(cRankId);

                if (cRankIndex >= rankIndex)
                {
                    newIndex = i;
                    break;
                }
            }

            if (newIndex == -1)
            {
                newIndex = characters.Count - 1;
            }
            else if (skipIndex)
            {
                newIndex--;
            }

            if (newIndex != -1 && newIndex != prevIndex)
            {
                characters.Move(character, newIndex);
            }
        }

        // == Guild Levels ==

        private void UpgradeBankRowCountCallback(int upgradeValue)
        {
            foreach (var bankTab in bankTabs)
            {
                var grid = bankTab.Grid;
                var rows = grid.DefaultRowsCount + upgradeValue;

                grid.SetSize(rows, grid.ColumnsCount);
            }
        }

        private void UpgradeCharactersCountCallback(int upgradeValue)
        {
            var count = config.MaxCharactersCount + upgradeValue;

            maxCharactersCount.Value = count;
        }

        // == Leaderboard ==

        public string CreateLeaderboardExtraData()
        {
            var data = new GuildScoreData
            {
                GuildName = guildName.Value,
                Emblem = Emblem
            };

            return JsonConvert.SerializeObject(data);
        }

        public static GuildScoreData GetLeaderboardExtraData(string json)
        {
            return JsonConvert.DeserializeObject<GuildScoreData>(json);
        }

        // == Save ==

        protected override GuildSM CreateSave()
        {
            var guildSM = new GuildSM
            {
                GuildName = GuildName.CurrentValue,
                PlayerName = PlayerName.CurrentValue,
                Emblem = Emblem
            };

            guildSM.SetCharacters(Characters, inventoryService);
            guildSM.SetBankTabs(BankTabs, inventoryService);

            return guildSM;
        }

        protected override void CreateDefaultState()
        {
            Emblem = new();

            bankTabs.AddRange(CreateDefaultBankTabs());
            guildRanks.AddRange(CreateDefaultGuildRanks());
        }

        protected override void ReadSave(GuildSM save)
        {
            guildName.Value = save.GuildName;
            playerName.Value = save.PlayerName;
            Emblem = save.Emblem;

            bankTabs.AddRange(save.GetBankTabs(config, inventoryService));
            characters.AddRange(save.GetCharacters(inventoryService));
            guildRanks.AddRange(CreateDefaultGuildRanks());
        }

        private IEnumerable<GuildRankInfo> CreateDefaultGuildRanks()
        {
            return config.DefaultGuildRanks.Select(x =>
            {
                return new GuildRankInfo(x);
            });
        }

        private IEnumerable<GuildBankTabInfo> CreateDefaultBankTabs()
        {
            return config.GuildBankParams.Tabs.Select(x =>
            {
                var grid = inventoryService.Factory.CreateGrid(x.Grid);

                return new GuildBankTabInfo(x, grid);
            });
        }
    }
}