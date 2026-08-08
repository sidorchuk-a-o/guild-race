using System.Threading;
using AD.Game.Inventory;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace AD.Game.Craft
{
    public class ProductContainer : MonoBehaviour
    {
        [SerializeField] private Image iconImage;

        [Header("Tooltip")]
        [SerializeField] private InventoryTooltipComponent tooltipComponent;

        private CraftVMFactory craftVMF;

        private ItemDataVM productVM;

        [Inject]
        public void Inject(CraftVMFactory craftVMF)
        {
            this.craftVMF = craftVMF;
        }

        public async UniTask Init(RecipeVM recipeVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            productVM = craftVMF.GetRecipeProduct(recipeVM.Id);
            productVM.AddTo(disp);

            var sprite = await productVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = sprite;
            tooltipComponent.Init(productVM);
        }
    }
}