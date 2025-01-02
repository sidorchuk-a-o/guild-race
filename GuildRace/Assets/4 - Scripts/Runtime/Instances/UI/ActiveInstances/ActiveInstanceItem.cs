#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Game.Instances
{
    public class ActiveInstanceItem : VMScrollItem<ActiveInstanceVM>
    {
        [Header("Instance")]
        [SerializeField] private UIText instanceNameText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            instanceNameText.SetTextParams(ViewModel.InstanceVM.NameKey);
        }
    }
}