#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Game.Craft
{
    public class RecipeScrollItem : VMScrollItem<RecipeVM>
    {
        [Header("Recipe")]
        [SerializeField] private UIText nameText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            nameText.SetTextParams(ViewModel.ProductVM.NameKey);
        }
    }
}