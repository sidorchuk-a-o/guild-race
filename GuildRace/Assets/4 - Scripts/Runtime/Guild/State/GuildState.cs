using AD.Services;
using AD.Services.Localization;
using AD.Services.Save;
using AD.ToolsCollection;
using Game.Items;
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

        private readonly RecruitingState recruitingState;

        private readonly IItemsDatabaseService itemsDatabase;
        private readonly ILocalizationService localization;

        public override string SaveKey => GuildSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public bool IsExists => name.IsValid();
        public IReadOnlyReactiveProperty<string> Name => name;

        public ICharactersCollection Characters => characters;
        public IGuildRanksCollection GuildRanks => guildRanks;

        public RecruitingState RecruitingState => recruitingState;

        public GuildState(
            GuildConfig config,
            IItemsDatabaseService itemsDatabase,
            ILocalizationService localization,
            IObjectResolver resolver)
            : base(config, resolver)
        {
            this.itemsDatabase = itemsDatabase;
            this.localization = localization;

            recruitingState = new(config, this, itemsDatabase);
        }

        public void CreateOrUpdateGuild(GuildEM guildEM)
        {
            name.Value = guildEM.Name;

            MarkAsDirty();
        }

        public int RemoveCharacter(string characterId)
        {
            var index = characters.FindIndex(x => x.Id == characterId);

            characters.RemoveAt(index);

            MarkAsDirty();

            return index;
        }

        public int AcceptJoinRequest(string requestId)
        {
            var request = recruitingState.Requests.FirstOrDefault(x => x.Id == requestId);

            characters.Add(request.Character);

            return RemoveJoinRequest(request.Id);
        }

        public int RemoveJoinRequest(string requestId)
        {
            return recruitingState.RemoveRequest(requestId);
        }

        // == Save ==

        protected override GuildSM CreateSave()
        {
            var guildSM = new GuildSM
            {
                Name = Name.Value,
                GuildRanks = GuildRanks,
                Recruiting = RecruitingState.CreateSave()
            };

            guildSM.SetCharacters(Characters, itemsDatabase);

            return guildSM;
        }

        protected override void ReadSave(GuildSM save)
        {
            if (save == null)
            {
                guildRanks.AddRange(CreateDefaultGuildRanks());

                recruitingState.ReadSave(null);
                return;
            }

            name.Value = save.Name;

            guildRanks.AddRange(save.GuildRanks);
            characters.AddRange(save.GetCharacters(itemsDatabase));

            recruitingState.ReadSave(save.Recruiting);
        }

        private IEnumerable<GuildRankInfo> CreateDefaultGuildRanks()
        {
            return config.DefaultGuildRanks.Select(x =>
            {
                var name = localization.Get(x.DefaultNameKey);

                return new GuildRankInfo(x.Id, name);
            });
        }
    }
}