using AD.Router;

namespace AD.Game.Instances
{
    public class InstancesVM : VMCollection<InstanceInfo, InstanceVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public InstancesVM(IInstancesCollection values, InstancesVMFactory instancesVMF) : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override InstanceVM Create(InstanceInfo value)
        {
            return new(value, instancesVMF);
        }
    }
}