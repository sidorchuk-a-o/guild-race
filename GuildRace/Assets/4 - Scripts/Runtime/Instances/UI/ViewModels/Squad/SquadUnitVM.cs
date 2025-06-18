using AD.Services.Router;
using Game.Guild;
using Game.Inventory;
using UniRx;

namespace Game.Instances
{
    public class SquadUnitVM : ViewModel
    {
        public string CharactedId { get; }
        public CharacterVM CharactedVM { get; }

        public ItemsGridVM BagVM { get; }
        public ThreatsVM ResolvedThreatsVM { get; }

        public SquadUnitVM(SquadUnitInfo info, InstancesVMFactory instancesVMF)
        {
            CharactedId = info.CharactedId;
            CharactedVM = instancesVMF.GuildVMF.GetCharacter(info.CharactedId);

            BagVM = instancesVMF.InventoryVMF.CreateItemsGrid(info.Bag);
            ResolvedThreatsVM = new ThreatsVM(info.ResolvedThreats, instancesVMF);
        }

        protected override void InitSubscribes()
        {
            CharactedVM.AddTo(this);
            BagVM.AddTo(this);
            ResolvedThreatsVM.AddTo(this);
        }
    }
}