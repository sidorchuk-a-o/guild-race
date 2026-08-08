using AD.Router;
using AD.ToolsCollection;
using R3;

namespace AD.Game.Guild
{
    public class CharactersVM : VMReactiveCollection<CharacterInfo, CharacterVM>
    {
        private readonly GuildVMFactory guildVMF;

        private readonly ReactiveProperty<string> countStr = new();

        public ReadOnlyReactiveProperty<string> CountStr => countStr;

        public CharactersVM(ICharactersCollection values, GuildVMFactory guildVMF)
            : base(values)
        {
            this.guildVMF = guildVMF;
        }

        protected override CharacterVM Create(CharacterInfo value)
        {
            return new(value, guildVMF);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            ObserveCountChanged()
                .Subscribe(CountChangedCallback)
                .AddTo(this);

            guildVMF.MaxCharactersCount
                .SilentSubscribe(CountChangedCallback)
                .AddTo(this);

            CountChangedCallback();
        }

        private void CountChangedCallback()
        {
            countStr.Value = $"{Count} / {guildVMF.MaxCharactersCount.CurrentValue}";
        }
    }
}