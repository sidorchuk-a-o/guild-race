using AD.Services;
using AD.Services.Localization;
using AD.Services.Save;
using AD.ToolsCollection;
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

        private readonly ILocalizationService localization;

        public override string SaveKey => GuildSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public bool IsExists => name.IsValid();
        public IReadOnlyReactiveProperty<string> Name => name;

        public ICharactersCollection Characters => characters;
        public IGuildRanksCollection GuildRanks => guildRanks;

        public GuildState(GuildConfig config, ILocalizationService localization, IObjectResolver resolver)
            : base(config, resolver)
        {
            this.localization = localization;
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

        // == Save ==

        protected override GuildSM CreateSave()
        {
            return new GuildSM
            {
                Name = Name.Value,
                Characters = Characters,
                GuildRanks = GuildRanks
            };
        }

        protected override void ReadSave(GuildSM save)
        {
            if (save == null)
            {
                guildRanks.AddRange(CreateDefaultGuildRanks());
                characters.AddRange(CreateDefaultCharacters());

                return;
            }

            name.Value = save.Name;

            guildRanks.AddRange(save.GuildRanks);
            characters.AddRange(save.Characters);
        }

        private IEnumerable<CharacterInfo> CreateDefaultCharacters()
        {
            var recruitRank = guildRanks.Last();

            return config.RecruitingModule.DefaultCharacters.Select(x =>
            {
                var id = GuidUtils.Generate();
                var nickname = $"Игрок ({id[..7]})";

                var info = new CharacterInfo(id, nickname, x.ClassId);

                info.SetGuildRank(recruitRank.Id);
                info.SetSpecialization(x.SpecId);

                return info;
            });
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