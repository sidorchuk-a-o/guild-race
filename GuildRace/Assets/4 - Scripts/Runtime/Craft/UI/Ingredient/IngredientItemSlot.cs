using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Craft
{
    public class IngredientItemSlot : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [Space]
        [SerializeField] private UIText countText;
        [SerializeField] private string countFormat = "{0} / <b><size=+4>{1}</size></b>";
        [SerializeField] private string countErrorFormat = "<color=#D92121>{0}</color> / <b><size=+4>{1}</size></b>";

        private readonly ReactiveProperty<bool> isAvailable = new();

        private IngredientVM ingredientVM;
        private int ingridientsCount;

        public IReadOnlyReactiveProperty<bool> IsAvailable => isAvailable;

        public async UniTask Init(IngredientVM ingredientVM, int craftingCount, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.ingredientVM = ingredientVM;

            // icon
            var sprite = await ingredientVM.ReagentVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = sprite;

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
            var format = IsAvailable.Value ? countFormat : countErrorFormat;
            var countStr = string.Format(format, ingredientVM.ReagentCounterVM.Count.Value, ingridientsCount);

            countText.SetTextParams(countStr);
        }
    }
}