using AD.Router;

namespace AD.Game.Instances
{
    public class SquadUnitsVM : VMReactiveCollection<SquadUnitInfo, SquadUnitVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public int MaxUnitsCount { get; }

        public SquadUnitsVM(InstanceType instanceType, ISquadUnitsCollection values, InstancesVMFactory instancesVMF)
            : base(values)
        {
            this.instancesVMF = instancesVMF;

            var instancesConfig = instancesVMF.InstancesConfig;
            var squadParams = instancesConfig.SquadParams.GetSquadParams(instanceType);

            MaxUnitsCount = squadParams.MaxUnitsCount;
        }

        protected override SquadUnitVM Create(SquadUnitInfo value)
        {
            return new(value, instancesVMF);
        }
    }
}