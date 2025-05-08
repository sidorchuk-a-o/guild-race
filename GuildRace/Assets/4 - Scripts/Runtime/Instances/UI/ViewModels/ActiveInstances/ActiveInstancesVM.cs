using AD.Services.Router;

namespace Game.Instances
{
    public class ActiveInstancesVM : VMReactiveCollection<ActiveInstanceInfo, ActiveInstanceVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public ActiveInstancesVM(IActiveInstancesCollection values, InstancesVMFactory instancesVMF)
            : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override ActiveInstanceVM Create(ActiveInstanceInfo value)
        {
            return new ActiveInstanceVM(value, instancesVMF);
        }
    }
}