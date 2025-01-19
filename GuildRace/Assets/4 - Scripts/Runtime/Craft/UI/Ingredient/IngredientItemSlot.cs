using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

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

        private CraftVMFactory craftVMF;

        private IngredientVM ingredientVM;
        private ItemCounterVM reagentCounterVM;
        private int ingridientsCount;

        public IReadOnlyReactiveProperty<bool> IsAvailable => isAvailable;

        [Inject]
        public void Inject(CraftVMFactory craftVMF)
        {
            this.craftVMF = craftVMF;
        }

        public async UniTask Init(
            IngredientVM ingredientVM,
            int craftingCount,
            CancellationTokenSource token,
            CompositeDisp disp)
        {
            this.ingredientVM = ingredientVM;

            // icon
            var sprite = await ingredientVM.ReagentVM.LoadIcon();

            if (token.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = sprite;

            // reagent
            reagentCounterVM = craftVMF.GetReagentItemCounter(ingredientVM.ReagentVM.Id);
            reagentCounterVM.AddTo(disp);

            reagentCounterVM.Count
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
            isAvailable.Value = reagentCounterVM.Count.Value >= ingridientsCount;
        }

        private void UpdateCountText()
        {
            var format = IsAvailable.Value ? countFormat : countErrorFormat;
            var countStr = string.Format(format, reagentCounterVM.Count.Value, ingridientsCount);

            countText.SetTextParams(countStr);
        }
    }
}