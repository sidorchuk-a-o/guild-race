using AD.Game.Instances;
using AD.Localization;
using AD.Router;

namespace AD.Game.Guild
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
            ThreatVM = instancesVMF.GetThreat(data.ThreatId);
        }

        protected override void InitSubscribes()
        {
            ThreatVM.AddTo(this);
        }
    }
}