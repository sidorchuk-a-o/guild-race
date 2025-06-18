using AD.ToolsCollection;
using AD.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Craft
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

        private readonly ReactiveProperty<bool> isAvailable = new();

        private IngredientVM ingredientVM;
        private int ingridientsCount;

        public IReadOnlyReactiveProperty<bool> IsAvailable => isAvailable;

        public async UniTask Init(IngredientVM ingredientVM, int craftingCount, CompositeDisp disp, CancellationTokenSource ct)
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

            // reagent
            ingredientVM.ReagentCounterVM.Count
                .SilentSubscribe(UpdateView)
                .AddTo(disp);

            // count
            SetCraftingCount(craftingCount);
        }

        public void SetCraftingCount(int count)
        {
            ingridientsCount = ingredientVM.Count * count;

            UpdateView();
        }

        private void UpdateView()
        {
            UpdateAvailableState();
            UpdateCountText();
        }

        private void UpdateAvailableState()
        {
            isAvailable.Value = ingredientVM.ReagentCounterVM.Count.Value >= ingridientsCount;
        }

        private void UpdateCountText()
        {
            var format = IsAvailable.Value ? craftCountFormat : craftCountErrorFormat;
            var countStr = string.Format(format, ingredientVM.ReagentCounterVM.Count.Value, ingridientsCount);

            craftCountText.SetTextParams(countStr);
        }
    }
}