using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using Game.Instances;

namespace Game.Guild
{
    public class AbilityVM : ViewModel
    {
        public int Id { get; }

        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public ThreatDataVM ThreatVM { get; }

        public AbilityVM(AbilityData data, InstancesVMFactory instancesVMF)
        {
            Id = data.Id;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            ThreatVM = new ThreatDataVM(instancesVMF.InstancesConfig.GetThreat(data.ThreatId));
        }

        protected override void InitSubscribes()
        {
            ThreatVM.AddTo(this);
        }
    }
}