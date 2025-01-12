using AD.Services.Router;

namespace Game.Instances
{
    public class InstancesVM : VMCollection<InstanceInfo, InstanceVM>
    {
        public InstancesVM(IInstancesCollection values) : base(values)
        {
        }

        protected override InstanceVM Create(InstanceInfo value)
        {
            return new InstanceVM(value);
        }
    }
}