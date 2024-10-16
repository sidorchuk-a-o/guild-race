using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Guild
{
    public class CharactersVM : VMCollection<CharacterInfo, CharacterVM>
    {
        private readonly GuildVMFactory guildVMF;
        private readonly ReactiveProperty<string> countStr = new();

        public ReactiveProperty<string> CountStr => countStr;

        public CharactersVM(ICharactersCollection values, GuildVMFactory guildVMF) : base(values)
        {
            this.guildVMF = guildVMF;
        }

        protected override CharacterVM Create(CharacterInfo value)
        {
            return new CharacterVM(value, guildVMF);
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            base.InitSubscribes(disp);

            ObserveCountChanged()
                .Subscribe(CountChangedCallback)
                .AddTo(disp);

            CountChangedCallback(Count);
        }

        private void CountChangedCallback(int count)
        {
            countStr.Value = $"{count} / {guildVMF.MaxCharactersCount}";
        }
    }
}