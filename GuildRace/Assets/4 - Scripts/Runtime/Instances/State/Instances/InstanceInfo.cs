using AD.Services.Localization;

namespace Game.Instances
{
    public class InstanceInfo
    {
        public int Id { get; }

        public InstanceType Type { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public InstanceInfo(InstanceData data)
        {
            Id = data.Id;
            Type = data.Type;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
        }
    }
}