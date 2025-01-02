using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class SquadRolesCountersContainer : MonoBehaviour
    {
        [SerializeField] private List<SquadRoleCounterItem> counters;

        private InstancesVMFactory instancesVMF;
        private SquadRolesCountersVM countersVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        public void Init(ActiveInstanceVM activeInstanceVM, CompositeDisp disp)
        {
            countersVM = instancesVMF.GetRolesCounters(activeInstanceVM.Id);
            countersVM.AddTo(disp);

            foreach (var counterVM in countersVM.Counters)
            {
                var counter = counters.First(x => x.Role == counterVM.RoleVM.Id);

                counter.Init(counterVM, disp);
            }
        }
    }
}