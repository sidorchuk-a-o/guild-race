using System.Threading;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class RewardItem : MonoBehaviour
    {
        [SerializeField] private RewardMechanicsUIParams rewardParams;

        private InstancesVMFactory instancesVMF;
        private RewardItemContent itemContent;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        public async void Init(InstanceRewardVM rewardVM, CancellationTokenSource ct)
        {
            if (itemContent)
            {
                instancesVMF.ReturnObject(itemContent);
                itemContent = null;
            }

            var mechanicParams = rewardParams.GetParams(rewardVM.MechanicVM.Id);
            var mechanicContentGO = await instancesVMF.RentObjectAsync(mechanicParams.ItemRef);

            if (ct.IsCancellationRequested)
            {
                instancesVMF.ReturnObject(mechanicContentGO);
                return;
            }

            itemContent = mechanicContentGO.GetComponent<RewardItemContent>();
            itemContent.SetParent(transform);

            var rect = itemContent.transform as RectTransform;
            rect.anchoredPosition = Vector2.zero;
            rect.SetAsLastSibling();

            await itemContent.Init(rewardVM, ct);
        }
    }
}