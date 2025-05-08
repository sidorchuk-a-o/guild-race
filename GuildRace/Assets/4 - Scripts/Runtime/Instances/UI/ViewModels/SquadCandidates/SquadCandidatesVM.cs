using System.Collections.Generic;
using AD.Services.Router;

namespace Game.Instances
{
    public class SquadCandidatesVM : VMCollection<SquadCandidateInfo, SquadCandidateVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public SquadCandidatesVM(IReadOnlyCollection<SquadCandidateInfo> values, InstancesVMFactory instancesVMF)
            : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override SquadCandidateVM Create(SquadCandidateInfo value)
        {
            return new SquadCandidateVM(value, instancesVMF);
        }
    }
}