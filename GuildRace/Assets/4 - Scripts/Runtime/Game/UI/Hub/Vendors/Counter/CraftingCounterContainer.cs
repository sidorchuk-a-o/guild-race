using AD.ToolsCollection;
using AD.UI;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Craft
{
    public class CraftingCounterContainer : MonoBehaviour
    {
        [SerializeField] private UIText countText;
        [SerializeField] private List<IncreaseButton> increaseButtons;

        private readonly ReactiveProperty<int> count = new();

        public IReadOnlyReactiveProperty<int> Count => count;

        public void Init(CompositeDisp disp)
        {
            foreach (var button in increaseButtons)
            {
                button.OnClick
                    .Subscribe(IncreaseCallback)
                    .AddTo(disp);
            }

            SetCount(1);
        }

        private void IncreaseCallback(int value)
        {
            AddCount(value);
        }

        private void AddCount(int value)
        {
            SetCount(count.Value + value);
        }

        private void SetCount(int newValue)
        {
            count.Value = Mathf.Clamp(newValue, 1, 500);

            countText.SetTextParams(count.Value);
        }
    }
}