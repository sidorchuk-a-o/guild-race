using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Router;
using AD.Services.Store;
using AD.ToolsCollection;
using Game.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Store
{
    public class StoreItemTooltip : TooltipContainer
    {
        [Header("Store Item Params")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;
        [Space]
        [SerializeField] private GameObject rewardSeparator;
        [SerializeField] private RewardPreviewsContainer rewardPreviewsContainer;

        public override async UniTask Init(ViewModel viewModel, CompositeDisp disp, CancellationTokenSource ct)
        {
            var storeItemVM = viewModel as StoreItemVM;
            var rewardVM = storeItemVM.RewardVM;

            var icon = await storeItemVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(storeItemVM.NameKey);
            descText.SetTextParams(storeItemVM.DescKey);

            await rewardPreviewsContainer.Init(rewardVM, disp, ct);

            rewardSeparator.SetActive(rewardVM.RewardPreviewsVM.Any());
        }
    }
}