using System.Threading;
using AD.Game.Inventory;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Craft
{
    public class IngredientItemSlot : MonoBehaviour
    {
        [Header("Reagent")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText countText;
        [SerializeField] private Image rarityImage;
        [Space]
        [SerializeField] private InventoryTooltipComponent tooltipComponent;

        [Header("Craft")]
        [SerializeField] private UIText craftCountText;
        [SerializeField] private string craftCountFormat = "{0} / <b><size=+4>{1}</size></b>";
        [SerializeField] private string craftCountErrorFormat = "<color=#D92121>{0}</color> / <b><size=+4>{1}</size></b>";

        private IngredientVM ingredientVM;

        public async UniTask Init(IngredientVM ingredientVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.ingredientVM = ingredientVM;

            // icon
            var reagentVM = ingredientVM.ReagentVM;
            var sprite = await reagentVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = sprite;
            rarityImage.color = reagentVM.RarityVM.Color;
            countText.SetTextParams(ingredientVM.Count);
            tooltipComponent.Init(reagentVM);

            ingredientVM.ReagentCounterVM.Count
                .SilentSubscribe(UpdateView)
                .AddTo(disp);

            ingredientVM.CraftCount
                .Subscribe(UpdateView)
                .AddTo(disp);
        }

        private void UpdateView()
        {
            var isAvailable = ingredientVM.IsAvailable.CurrentValue;
            var reagentsCount = ingredientVM.ReagentCounterVM.Count.CurrentValue;
            var craftsCount = ingredientVM.CraftCount.CurrentValue;

            var format = isAvailable ? craftCountFormat : craftCountErrorFormat;
            var countStr = string.Format(format, reagentsCount, craftsCount);

            craftCountText.SetTextParams(countStr);
        }
    }
}