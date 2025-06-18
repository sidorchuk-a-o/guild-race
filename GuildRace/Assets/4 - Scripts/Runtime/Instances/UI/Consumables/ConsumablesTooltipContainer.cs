using System.Threading;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class ConsumablesTooltipContainer : ItemTooltipContainer
    {
        [Header("Consumables")]
        [SerializeField] private RarityComponent rarityComponent;
        [SerializeField] private TooltipItemStackComponent stackComponent;
        [SerializeField] private ConsumableChanceDiffComponent consumableChanceDiffComponent;
        [Space]
        [SerializeField] private RectTransform mechanicContainerRoot;
        [SerializeField] private ConsumableMechanicsUIParams mechanicsUIParams;

        private InstancesVMFactory instancesVMF;
        private ConsumableMechanicContainer mechanicContainer;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        public override async UniTask Init(TooltipContext context, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(context, disp, ct);

            if (ct.IsCancellationRequested) return;

            var dataVM = context.DataVM as ConsumablesDataVM;

            stackComponent.Init(context, disp);
            rarityComponent.Init(dataVM.RarityVM);

            if (consumableChanceDiffComponent)
            {
                consumableChanceDiffComponent.Init(context);
            }

            if (mechanicContainer)
            {
                instancesVMF.ReturnObject(mechanicContainer);
                mechanicContainer = null;
            }

            var mechanicParams = mechanicsUIParams.GetParams(dataVM.MechanicVM.Id);
            var mechanicContainerGO = await instancesVMF.RentObjectAsync(mechanicParams.ContainerRef);

            if (ct.IsCancellationRequested)
            {
                instancesVMF.ReturnObject(mechanicContainerGO);
                return;
            }

            mechanicContainer = mechanicContainerGO.GetComponent<ConsumableMechanicContainer>();

            mechanicContainer.SetParent(transform);
            mechanicContainer.transform.SetSiblingIndex(mechanicContainerRoot.GetSiblingIndex());

            await mechanicContainer.Init(dataVM, disp, ct);
        }
    }
}