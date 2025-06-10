#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class ActiveInstanceScrollItem : VMScrollItem<ActiveInstanceVM>
    {
        [Header("Instance")]
        [SerializeField] private UIText instanceNameText;
        [SerializeField] private UIText bossNameText;
        [SerializeField] private UIStates resultState;
        [Space]
        [SerializeField] private GameObject timerContainer;
        [SerializeField] private UIText timerText;

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            instanceNameText.SetTextParams(ViewModel.InstanceVM.NameKey);
            bossNameText.SetTextParams(ViewModel.BossUnitVM.NameKey);

            ViewModel.ResultStateVM.Value
                .Subscribe(resultState.SetState)
                .AddTo(disp);

            ViewModel.IsReadyToComplete
                .Subscribe(resultState.SetActive)
                .AddTo(disp);

            ViewModel.IsReadyToComplete
                .Subscribe(x => timerContainer.SetActive(!x))
                .AddTo(disp);

            ViewModel.TimerVM.Value
                .Subscribe(x => timerText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}