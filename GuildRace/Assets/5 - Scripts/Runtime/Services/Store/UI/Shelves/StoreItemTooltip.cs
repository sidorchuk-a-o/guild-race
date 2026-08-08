using System.Linq;
using System.Threading;
using AD.Game.UI;
using AD.Router;
using AD.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Store
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
            var storeItemVM = (StoreItemVM)viewModel;
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