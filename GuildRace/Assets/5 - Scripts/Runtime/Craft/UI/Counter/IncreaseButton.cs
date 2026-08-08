using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.Craft
{
    public class IncreaseButton : MonoBehaviour
    {
        [SerializeField] private int value;
        [SerializeField] private UIButton button;

        private readonly ReactiveEvent<int> onClick = new();

        public ReadOnlyReactiveEvent<int> OnClick => onClick;

        private void Awake()
        {
            button.OnClick
                .Subscribe(() => onClick.OnNext(value))
                .AddTo(this);
        }
    }
}