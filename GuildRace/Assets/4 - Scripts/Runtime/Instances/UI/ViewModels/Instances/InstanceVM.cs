using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;

namespace Game.Instances
{
    public class InstanceVM : ViewModel
    {
        public int Id { get; }

        public InstanceType Type { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public UnitsVM BossUnitsVM { get; }

        public InstanceVM(InstanceInfo info, InstancesVMFactory instancesVMF)
        {
            Id = info.Id;
            Type = info.Type;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
            BossUnitsVM = new UnitsVM(info.BossUnits, instancesVMF);
        }

        protected override void InitSubscribes()
        {
            BossUnitsVM.AddTo(this);
        }
    }
}