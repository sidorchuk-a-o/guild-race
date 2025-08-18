using AD.Services;
using AD.Services.Localization;
using AD.Services.Save;
using AD.ToolsCollection;
using Game.GuildLevels;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using VContainer;

namespace Game.Guild
{
    public class GuildState : ServiceState<GuildConfig, GuildSM>
    {
        private readonly ReactiveProperty<string> guildName = new();
        private readonly ReactiveProperty<string> playerName = new();

        private readonly ReactiveProperty<int> maxCharactersCount = new();
        private readonly CharactersCollection characters = new(null);
        private readonly GuildRanksCollection guildRanks = new(null);
        private readonly GuildBankTabsCollection bankTabs = new(null);

        private readonly GuildLevelContext guildLevelContext = new();

        private readonly IGuildLevelsService guildLevelsService;
        private readonly IInventoryService inventoryService;
        private readonly ILocalizationService localization;

        public override string SaveKey => GuildSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public bool IsExists => guildName.IsValid();
        public IReadOnlyReactiveProperty<string> GuildName => guildName;
        public IReadOnlyReactiveProperty<string> PlayerName => playerName;
        public IReadOnlyReactiveProperty<int> MaxCharactersCount => maxCharactersCount;
        public EmblemInfo Emblem { get; private set; }

        public ICharactersCollection Characters => characters;
        public IGuildRanksCollection GuildRanks => guildRanks;
        public IGuildBankTabsCollection BankTabs => bankTabs;

        public GuildState(
            GuildConfig config,
            IGuildLevelsService guildLevelsService,
            IInventoryService inventoryService,
            ILocalizationService localization,
            IObjectResolver resolver)
            : base(config, resolver)
        {
            this.guildLevelsService = guildLevelsService;
            this.inventoryService = inventoryService;
            this.localization = localization;
        }

        public override void Init()
        {
            guildLevelsService.RegisterContext(guildLevelContext);
            guildLevelContext.CharactersCount.Subscribe(UpgradeCharactersCountCallback);

            base.Init();
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

        private void UpgradeCharactersCountCallback(int upgradeValue)
        {
            var count = config.MaxCharactersCount + upgradeValue;

            maxCharactersCount.Value = count;

            MarkAsDirty();
        }

        // == Save ==

        protected override GuildSM CreateSave()
        {
            var guildSM = new GuildSM
            {
                GuildName = GuildName.Value,
                PlayerName = PlayerName.Value,
                GuildRanks = GuildRanks,
                Emblem = Emblem
            };

            guildSM.SetCharacters(Characters, inventoryService);
            guildSM.SetBankTabs(BankTabs, inventoryService);

            return guildSM;
        }

        protected override void ReadSave(GuildSM save)
        {
            if (save == null)
            {
                Emblem = new();
                bankTabs.AddRange(CreateDefaultBankTabs());
                guildRanks.AddRange(CreateDefaultGuildRanks());

                return;
            }

            guildName.Value = save.GuildName;
            playerName.Value = save.PlayerName;
            Emblem = save.Emblem;

            guildRanks.AddRange(save.GuildRanks);
            characters.AddRange(save.GetCharacters(inventoryService));
            bankTabs.AddRange(save.GetBankTabs(config, inventoryService));
        }

        private IEnumerable<GuildRankInfo> CreateDefaultGuildRanks()
        {
            return config.DefaultGuildRanks.Select(x =>
            {
                var name = localization.Get(x.DefaultNameKey);

                return new GuildRankInfo(x.Id, name);
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