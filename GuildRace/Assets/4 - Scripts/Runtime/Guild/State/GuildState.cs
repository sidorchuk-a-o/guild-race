using AD.Services;
using AD.Services.Localization;
using AD.Services.Save;
using AD.ToolsCollection;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using VContainer;

namespace Game.Guild
{
    public class GuildState : ServiceState<GuildConfig, GuildSM>
    {
        private readonly ReactiveProperty<string> name = new();
        private readonly CharactersCollection characters = new(null);
        private readonly GuildRanksCollection guildRanks = new(null);
        private readonly GuildBankTabsCollection bankTabs = new(null);

        private readonly IInventoryService inventoryService;
        private readonly ILocalizationService localization;

        public override string SaveKey => GuildSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public bool IsExists => name.IsValid();
        public IReadOnlyReactiveProperty<string> Name => name;
        public EmblemInfo Emblem { get; private set; }

        public ICharactersCollection Characters => characters;
        public IGuildRanksCollection GuildRanks => guildRanks;
        public IGuildBankTabsCollection BankTabs => bankTabs;

        public GuildState(
            GuildConfig config,
            IInventoryService inventoryService,
            ILocalizationService localization,
            IObjectResolver resolver)
            : base(config, resolver)
        {
            this.inventoryService = inventoryService;
            this.localization = localization;
        }

        public void CreateOrUpdateGuild(GuildEM guildEM)
        {
            name.Value = guildEM.Name;

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

        // == Character ==

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

        // == Save ==

        protected override GuildSM CreateSave()
        {
            var guildSM = new GuildSM
            {
                Name = Name.Value,
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

            name.Value = save.Name;
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