using AD.UI;
using AD.ToolsCollection;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;
using System;
using System.Threading;

namespace Game.Tutorial
{
    public class TapToCloseContainer : MonoBehaviour
    {
        [SerializeField] private float activateDelay = 2;
        [SerializeField] private UIButton tapArea;

        private ITutorialService tutorialService;

        private CancellationTokenSource containerToken;
        private string containerId;

        [Inject]
        public void Inject(ITutorialService tutorialService)
        {
            this.tutorialService = tutorialService;
        }

        private void Awake()
        {
            tapArea.SetActive(false);

            tapArea.OnClick
                .Subscribe(TapCallback)
                .AddTo(this);
        }

        private async void OnEnable()
        {
            var token = new CancellationTokenSource();

            containerToken?.Cancel();
            containerToken = token;

            await UniTask.Delay(TimeSpan.FromSeconds(activateDelay));

            if (token.IsCancellationRequested)
            {
                return;
            }

            tapArea.SetActive(true);
        }

        private void OnDisable()
        {
            containerToken?.Cancel();

            tapArea.SetActive(false);
        }

        public void SetContainerId(string containerId)
        {
            this.containerId = containerId;
        }

        private void TapCallback()
        {
            tutorialService.MarkContainerAsShowed(containerId);
        }
    }
}