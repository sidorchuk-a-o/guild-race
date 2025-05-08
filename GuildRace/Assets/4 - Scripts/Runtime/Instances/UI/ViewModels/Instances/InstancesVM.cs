using AD.Services.Router;

namespace Game.Instances
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
            return new InstanceVM(value, instancesVMF);
        }
    }
}