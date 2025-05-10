using System;
using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class ContentTab : MonoBehaviour
    {
        [SerializeField] private CanvasGroup content;

        [Header("Button")]
        [SerializeField] private UIButton button;
        [SerializeField] private string selectedStateKey = "selected";
        [SerializeField] private string unselectedStateKey = "default";

        private readonly Subject<ContentTab> onClick = new();

        public IObservable<ContentTab> OnClick => onClick;

        private void Awake()
        {
            button.OnClick
                .Subscribe(x => onClick.OnNext(this))
                .AddTo(this);
        }

        public void SetContentState(bool state)
        {
            content.alpha = state ? 1 : 0;
            content.interactable = state;
            content.blocksRaycasts = state;

            button.SetState(state ? selectedStateKey : unselectedStateKey);
        }
    }
}