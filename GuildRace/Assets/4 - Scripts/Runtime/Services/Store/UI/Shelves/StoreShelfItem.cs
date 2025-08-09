#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.Threading;
using AD.Services.Router;
using AD.Services.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Store
{
    public class StoreShelfItem : VMScrollItem<StoreShelfVM>
    {
        [Header("Shelf")]
        [SerializeField] private UIText titleText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            titleText.SetTextParams(ViewModel.TitleKey);
        }
    }
}