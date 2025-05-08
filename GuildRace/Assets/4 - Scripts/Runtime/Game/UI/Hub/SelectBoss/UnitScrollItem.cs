#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Instances
{
    public class UnitScrollItem : VMScrollItem<UnitVM>
    {
        [Header("Unit")]
        [SerializeField] private UIText nameText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            nameText.SetTextParams(ViewModel.NameKey);
        }
    }
}