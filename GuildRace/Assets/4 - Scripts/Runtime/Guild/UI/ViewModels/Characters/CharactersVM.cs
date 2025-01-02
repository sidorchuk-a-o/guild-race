using AD.Services.Router;
using AD.ToolsCollection;
using Game.Instances;
using Game.Inventory;
using UniRx;

namespace Game.Guild
{
    public class CharactersVM : VMCollection<CharacterInfo, CharacterVM>
    {
        private readonly GuildVMFactory guildVMF;
        private readonly InventoryVMFactory inventoryVMF;
        private readonly InstancesVMFactory instancesVMF;

        private readonly ReactiveProperty<string> countStr = new();

        public ReactiveProperty<string> CountStr => countStr;

        public CharactersVM(
            ICharactersCollection values,
            GuildVMFactory guildVMF,
            InventoryVMFactory inventoryVMF,
            InstancesVMFactory instancesVMF)
            : base(values)
        {
            this.guildVMF = guildVMF;
            this.inventoryVMF = inventoryVMF;
            this.instancesVMF = instancesVMF;
        }

        protected override CharacterVM Create(CharacterInfo value)
        {
            return new CharacterVM(value, guildVMF, inventoryVMF, instancesVMF);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            ObserveCountChanged()
                .Subscribe(CountChangedCallback)
                .AddTo(this);

            CountChangedCallback(Count);
        }

        private void CountChangedCallback(int count)
        {
            countStr.Value = $"{count} / {guildVMF.MaxCharactersCount}";
        }
    }
}