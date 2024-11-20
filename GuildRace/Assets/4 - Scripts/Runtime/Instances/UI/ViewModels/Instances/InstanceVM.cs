using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Instances
{
    public class InstanceVM : ViewModel
    {
        public int Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public InstanceVM(InstanceInfo info)
        {
            Id = info.Id;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
        }

        protected override void InitSubscribes()
        {
        }
    }
}