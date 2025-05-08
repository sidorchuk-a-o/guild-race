using AD.Services.Router;

namespace Game.Instances
{
    public class InstancesVM : VMCollection<InstanceInfo, InstanceVM>
    {
        private readonly InstancesConfig config;

        public InstancesVM(IInstancesCollection values, InstancesConfig config) : base(values)
        {
            this.config = config;
        }

        protected override InstanceVM Create(InstanceInfo value)
        {
            return new InstanceVM(value, config);
        }
    }
}