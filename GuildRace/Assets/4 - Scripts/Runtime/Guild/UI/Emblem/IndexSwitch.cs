using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Guild
{
    public class IndexSwitch : MonoBehaviour
    {
        [SerializeField] private UIText valueText;
        [SerializeField] private UIButton prevButton;
        [SerializeField] private UIButton nextButton;

        private int maxIndex;
        private readonly ReactiveProperty<int> index = new();

        public IReadOnlyReactiveProperty<int> Index => index;

        private void Awake()
        {
            prevButton.OnClick
                .Subscribe(() => SetValue(index.Value - 1))
                .AddTo(this);

            nextButton.OnClick
                .Subscribe(() => SetValue(index.Value + 1))
                .AddTo(this);
        }

        public void Init(int maxCount, int value)
        {
            maxIndex = maxCount - 1;

            SetValue(value);
        }

        private void SetValue(int index)
        {
            if (index < 0)
            {
                index = maxIndex;
            }

            if (index > maxIndex)
            {
                index = 0;
            }

            this.index.Value = index;

            valueText.SetTextParams(index + 1);
        }
    }
}