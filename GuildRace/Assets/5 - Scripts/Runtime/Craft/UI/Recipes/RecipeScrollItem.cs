using System.Threading;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Craft
{
    public class RecipeScrollItem : VMScrollItem<RecipeVM>
    {
        [Header("Recipe")]
        [SerializeField] private Image itemIconImage;
        [SerializeField] private UIText nameText;
        [Space]
        [SerializeField] private GameObject availableCountContainer;
        [SerializeField] private UIText availableCountText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            var icon = await ViewModel.ProductVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            itemIconImage.sprite = icon;
            nameText.SetTextParams(ViewModel.ProductVM.NameKey);

            ViewModel.AvailableCount
                .Subscribe(x => availableCountContainer.SetActive(x > 0))
                .AddTo(disp);

            ViewModel.AvailableCountStr
                .Subscribe(x => availableCountText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}