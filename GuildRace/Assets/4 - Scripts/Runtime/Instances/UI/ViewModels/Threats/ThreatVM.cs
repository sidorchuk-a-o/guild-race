using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Game.Instances
{
    public class ThreatVM : ViewModel
    {
        public ThreatDataVM ThreatDataVM { get; }
        public IReadOnlyReactiveProperty<bool> Resolved { get; }

        public ThreatVM(ThreatInfo info, InstancesVMFactory instancesVMF)
        {
            Resolved = info.Resolved;
            ThreatDataVM = new ThreatDataVM(instancesVMF.InstancesConfig.GetThreat(info.Id));
        }

        protected override void InitSubscribes()
        {
            ThreatDataVM.AddTo(this);
        }
    }
}