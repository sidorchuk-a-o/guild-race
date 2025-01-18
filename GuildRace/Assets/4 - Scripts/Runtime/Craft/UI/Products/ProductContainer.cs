using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Craft
{
    public class ProductContainer : MonoBehaviour
    {
        [SerializeField] private Image iconImage;

        private CraftVMFactory craftVMF;

        private ItemDataVM productVM;

        [Inject]
        public void Inject(CraftVMFactory craftVMF)
        {
            this.craftVMF = craftVMF;
        }

        public async UniTask Init(RecipeVM recipeVM, CancellationTokenSource token, CompositeDisp disp)
        {
            productVM = craftVMF.GetRecipeProduct(recipeVM.Id);
            productVM.AddTo(disp);

            var sprite = await productVM.LoadIcon();

            if (token.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = sprite;
        }
    }
}