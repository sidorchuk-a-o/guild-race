using AD.UI;
using AD.ToolsCollection;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;
using System;

namespace Game.Tutorial
{
    public class TapToCloseContainer : MonoBehaviour
    {
        [SerializeField] private float activateDelay = 2;
        [SerializeField] private UIButton tapArea;

        private string containerId;
        private ITutorialService tutorialService;

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

        private async void Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(activateDelay));

            tapArea.SetActive(true);
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