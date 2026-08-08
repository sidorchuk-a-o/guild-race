using AD.Components;
using AD.Game.UI;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;

namespace AD.Game.Instances
{
    public class CompletedInstancesCounter : GameComponent<CompletedInstancesCounter>
    {
        [SerializeField] private CounterComponent counter;

        private ActiveInstancesVM activeInstancesVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            activeInstancesVM = instancesVMF.GetActiveInstances();
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            base.InitSubscribes(disp);

            activeInstancesVM.AddTo(disp);

            counter.Init(activeInstancesVM.CompletedCount, disp);
        }
    }
}