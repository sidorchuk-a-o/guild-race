using AD.Router;
using R3;

namespace AD.Game.Instances
{
    public class ThreatVM : ViewModel
    {
        public ThreatDataVM ThreatDataVM { get; }
        public ReadOnlyReactiveProperty<bool> Resolved { get; }

        public ThreatVM(ThreatInfo info, InstancesVMFactory instancesVMF)
        {
            Resolved = info.Resolved;
            ThreatDataVM = instancesVMF.GetThreat(info.Id);
        }

        protected override void InitSubscribes()
        {
            ThreatDataVM.AddTo(this);
        }
    }
}