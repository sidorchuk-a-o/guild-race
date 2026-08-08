using System.Threading;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace AD.Game.Instances
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

        protected override UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
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

            ViewModel.TimerVM.TimeLeftStr
                .Subscribe(x => timerText.SetTextParams(x))
                .AddTo(disp);

            return UniTask.CompletedTask;
        }
    }
}