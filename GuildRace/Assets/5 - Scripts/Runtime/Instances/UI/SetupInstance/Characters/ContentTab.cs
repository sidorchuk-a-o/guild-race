using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.Instances
{
    public class ContentTab : MonoBehaviour
    {
        [SerializeField] private CanvasGroup content;

        [Header("Button")]
        [SerializeField] private UIButton button;
        [SerializeField] private string selectedStateKey = "selected";
        [SerializeField] private string unselectedStateKey = "default";

        private readonly ReactiveEvent<ContentTab> onClick = new();

        public ReadOnlyReactiveEvent<ContentTab> OnClick => onClick;

        private void Awake()
        {
            button.OnClick
                .Subscribe(_ => onClick.OnNext(this))
                .AddTo(this);
        }

        public void SetContentState(bool state)
        {
            content.alpha = state ? 1 : 0;
            content.SetInteractable(state);

            button.SetState(state ? selectedStateKey : unselectedStateKey);
        }
    }
}