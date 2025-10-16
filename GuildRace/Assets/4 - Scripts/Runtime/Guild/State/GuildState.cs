using AD.Services;
using AD.Services.Save;
using AD.ToolsCollection;
using Game.GuildLevels;
using Game.Inventory;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using VContainer;
using UniRx;

namespace Game.Guild
{
    public class GuildState : ServiceState<GuildConfig, GuildSM>
    {
        private readonly ReactiveProperty<string> guildName = new();
        private readonly ReactiveProperty<string> playerName = new();

        private readonly ReactiveProperty<int> maxCharactersCount = new();
        private readonly ReactiveProperty<float> requestTimePercent = new(1);

        private readonly CharactersCollection characters = new(null);
        private readonly GuildRanksCollection guildRanks = new(null);
        private readonly GuildBankTabsCollection bankTabs = new(null);

        private readonly GuildLevelContext levelContext = new();

        private readonly IGuildLevelsService guildLevelsService;
        private readonly IInventoryService inventoryService;

        public override string SaveKey => GuildSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public bool IsExists => guildName.IsValid();
        public IReadOnlyReactiveProperty<string> GuildName => guildName;
        public IReadOnlyReactiveProperty<string> PlayerName => playerName;

        public IReadOnlyReactiveProperty<int> MaxCharactersCount => maxCharactersCount;
        public IReadOnlyReactiveProperty<float> RequestTimePercent => requestTimePercent;

        public ICharactersCollection Characters => characters;
        public IGuildRanksCollection GuildRanks => guildRanks;
        public IGuildBankTabsCollection BankTabs => bankTabs;
        public EmblemInfo Emblem { get; private set; }

        public GuildState(
            GuildConfig config,
            IGuildLevelsService guildLevelsService,
            IInventoryService inventoryService,
            IObjectResolver resolver)
            : base(config, resolver)
        {
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
            return new EmblemEM(Emblem);
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

        public string CreateLeaderboeardExtraData()
        {
            var data = new GuildScoreData
            {
                GuildName = guildName.Value,
                Emblem = Emblem
            };

            return JsonConvert.SerializeObject(data);
        }

        public GuildScoreData GetLeaderboeardExtraData(string json)
        {
            return JsonConvert.DeserializeObject<GuildScoreData>(json);
        }

        // == Save ==

        protected override GuildSM CreateSave()
        {
            var guildSM = new GuildSM
            {
                GuildName = GuildName.Value,
                PlayerName = PlayerName.Value,
                Emblem = Emblem
            };

            guildSM.SetCharacters(Characters, inventoryService);
            guildSM.SetBankTabs(BankTabs, inventoryService);

            return guildSM;
        }

        protected override void ReadSave(GuildSM save)
        {
            guildRanks.AddRange(CreateDefaultGuildRanks());

            if (save == null)
            {
                Emblem = new();
                bankTabs.AddRange(CreateDefaultBankTabs());

                return;
            }

            guildName.Value = save.GuildName;
            playerName.Value = save.PlayerName;
            Emblem = save.Emblem;

            characters.AddRange(save.GetCharacters(inventoryService));
            bankTabs.AddRange(save.GetBankTabs(config, inventoryService));
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