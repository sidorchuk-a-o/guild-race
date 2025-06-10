using AD.Services.Router;

namespace Game.Instances
{
    public class ThreatConsumableVM : ConsumableMechanicVM
    {
        public ThreatDataVM ThreatVM { get; }

        public ThreatConsumableVM(ConsumablesItemInfo info, ThreatConsumableHandler handler, InstancesVMFactory instancesVMF)
            : base(info, handler, instancesVMF)
        {
            ThreatVM = instancesVMF.GetThreat(handler.GetThreat(info));
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            ThreatVM.AddTo(this);
        }
    }
}