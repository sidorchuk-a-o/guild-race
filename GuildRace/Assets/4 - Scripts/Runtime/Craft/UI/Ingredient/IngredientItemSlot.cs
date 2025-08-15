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
            var format = ingredientVM.IsAvailable.Value ? craftCountFormat : craftCountErrorFormat;
            var countStr = string.Format(format, ingredientVM.ReagentCounterVM.Count.Value, ingredientVM.CraftCount.Value);

            craftCountText.SetTextParams(countStr);
        }
    }
}