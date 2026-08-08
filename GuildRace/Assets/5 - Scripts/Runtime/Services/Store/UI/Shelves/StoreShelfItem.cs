using System.Threading;
using AD.Router;
using AD.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Store
{
    public class StoreShelfItem : VMScrollItem<StoreShelfVM>
    {
        [Header("Shelf")]
        [SerializeField] private UIText titleText;

        protected override UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            titleText.SetTextParams(ViewModel.TitleKey);

            return UniTask.CompletedTask;
        }
    }
}