using AD.Services.Router;
using AD.ToolsCollection;
using Game.Items;
using UniRx;

namespace Game.Guild
{
    public class CharactersVM : VMCollection<CharacterInfo, CharacterVM>
    {
        private readonly GuildVMFactory guildVMF;
        private readonly ItemsVMFactory itemsVMF;

        private readonly ReactiveProperty<string> countStr = new();

        public ReactiveProperty<string> CountStr => countStr;

        public CharactersVM(
            ICharactersCollection values,
            GuildVMFactory guildVMF,
            ItemsVMFactory itemsVMF)
            : base(values)
        {
            this.guildVMF = guildVMF;
            this.itemsVMF = itemsVMF;
        }

        protected override CharacterVM Create(CharacterInfo value)
        {
            return new CharacterVM(value, guildVMF, itemsVMF);
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