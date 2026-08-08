using AD.Router;

namespace AD.Game.Instances
{
    public class ThreatConsumableVM : ConsumableMechanicVM
    {
        public ThreatDataVM ThreatVM { get; }

        public ThreatConsumableVM(ConsumablesItemData data, ThreatConsumableHandler handler, InstancesVMFactory instancesVMF)
            : base(data, handler, instancesVMF)
        {
            ThreatVM = instancesVMF.GetThreat(ThreatConsumableHandler.GetThreat(data));
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            ThreatVM.AddTo(this);
        }
    }
}