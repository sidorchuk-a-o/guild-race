using System.Threading;
using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;

namespace Game.Store
{
    public class RewardResultItem : MonoBehaviour
    {
        [SerializeField] private RewardResultsUIParams resultParams;

        private StoreVMFactory storeVMF;
        private RewardItemContent itemContent;

        [Inject]
        public void Inject(StoreVMFactory storeVMF)
        {
            this.storeVMF = storeVMF;
        }

        public async void Init(RewardResultVM rewardVM, CancellationTokenSource ct)
        {
            if (itemContent)
            {
                storeVMF.ReturnObject(itemContent);
                itemContent = null;
            }

            var mechanicParams = resultParams.GetParams(rewardVM.RewardType);
            var mechanicContentGO = await storeVMF.RentObjectAsync(mechanicParams.ItemRef);

            if (ct.IsCancellationRequested)
            {
                storeVMF.ReturnObject(mechanicContentGO);
                return;
            }

            itemContent = mechanicContentGO.GetComponent<RewardItemContent>();
            itemContent.SetParent(transform);

            var rect = itemContent.transform as RectTransform;
            rect.transform.localScale = Vector3.one;
            rect.anchoredPosition = Vector2.zero;
            rect.SetAsLastSibling();

            await itemContent.Init(rewardVM, ct);
        }
    }
}