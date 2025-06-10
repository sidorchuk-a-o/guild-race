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

        private RecipeVM recipeVM;

        private readonly ReactiveProperty<int> count = new();

        public IReadOnlyReactiveProperty<int> Count => count;

        public void Init(RecipeVM recipeVM, CompositeDisp disp)
        {
            this.recipeVM = recipeVM;

            foreach (var button in increaseButtons)
            {
                button.OnClick
                    .Subscribe(IncreaseCallback)
                    .AddTo(disp);
            }

            ResetCount();
        }

        public void ResetCount()
        {
            SetCount(0);
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
            count.Value = Mathf.Clamp(newValue, 0, recipeVM.AvailableCount.Value);

            countText.SetTextParams(count.Value);
        }
    }
}