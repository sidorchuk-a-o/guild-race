using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Game.Instances
{
    public class ThreatVM : ViewModel
    {
        public ThreatDataVM ThreatDataVM { get; }
        public IReadOnlyReactiveProperty<bool> Resolved { get; }

        public ThreatVM(ThreatInfo info, InstancesConfig instancesConfig)
        {
            Resolved = info.Resolved;
            ThreatDataVM = new ThreatDataVM(instancesConfig.GetThreat(info.ThreatId));
        }

        protected override void InitSubscribes()
        {
            ThreatDataVM.AddTo(this);
        }
    }
}