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

        public override async UniTask Init(ItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(itemVM, disp, ct);

            if (ct.IsCancellationRequested) return;

            var consumableVM = itemVM as ConsumablesItemVM;

            stackComponent.Init(consumableVM, disp);
            rarityComponent.Init(consumableVM.RarityVM);

            if (mechanicContainer)
            {
                instancesVMF.ReturnObject(mechanicContainer);
                mechanicContainer = null;
            }

            var mechanicParams = mechanicsUIParams.GetParams(consumableVM.MechanicVM.Id);
            var mechanicContainerGO = await instancesVMF.RentObjectAsync(mechanicParams.ContainerRef);

            if (ct.IsCancellationRequested)
            {
                instancesVMF.ReturnObject(mechanicContainerGO);
                return;
            }

            mechanicContainer = mechanicContainerGO.GetComponent<ConsumableMechanicContainer>();

            mechanicContainer.SetParent(transform);
            mechanicContainer.transform.SetSiblingIndex(mechanicContainerRoot.GetSiblingIndex());

            await mechanicContainer.Init(consumableVM, disp, ct);
        }
    }
}