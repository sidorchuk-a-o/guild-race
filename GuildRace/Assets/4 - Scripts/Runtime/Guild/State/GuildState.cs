using AD.Services;
using AD.Services.Save;
using AD.ToolsCollection;
using UniRx;
using VContainer;

namespace Game.Guild
{
    public class GuildState : ServiceState<GuildConfig, GuildSM>
    {
        private readonly ReactiveProperty<string> name = new();

        public override string SaveKey => GuildSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public bool IsExists => name.IsValid();
        public IReadOnlyReactiveProperty<string> Name => name;

        public GuildState(GuildConfig config, IObjectResolver resolver) : base(config, resolver)
        {
        }

        public void CreateGuild(GuildEM guildEM)
        {
            name.Value = guildEM.Name;

            MarkAsDirty();
        }

        // == Save ==

        protected override GuildSM CreateSave()
        {
            return new GuildSM
            {
                Name = Name.Value
            };
        }

        protected override void ReadSave(GuildSM save)
        {
            if (save == null)
            {
                return;
            }

            name.Value = save.Name;
        }
    }
}