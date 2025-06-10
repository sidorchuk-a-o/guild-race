using AD.Services.Router;

namespace Game.Instances
{
    public class ThreatConsumableVM : ConsumableMechanicVM
    {
        public ThreatDataVM ThreatVM { get; }

        public ThreatConsumableVM(ConsumablesItemData data, ThreatConsumableHandler handler, InstancesVMFactory instancesVMF)
            : base(data, handler, instancesVMF)
        {
            ThreatVM = instancesVMF.GetThreat(handler.GetThreat(data));
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            ThreatVM.AddTo(this);
        }
    }
}