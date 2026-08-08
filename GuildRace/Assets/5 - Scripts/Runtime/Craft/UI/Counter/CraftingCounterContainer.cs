using System.Collections.Generic;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.Craft
{
    public class CraftingCounterContainer : MonoBehaviour
    {
        [SerializeField] private UIText countText;
        [SerializeField] private List<IncreaseButton> increaseButtons;

        private RecipeVM recipeVM;

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
            SetCount(recipeVM.Count.CurrentValue + value);
        }

        private void SetCount(int newValue)
        {
            var availableCount = recipeVM.AvailableCount.CurrentValue;
            var count = Mathf.Clamp(newValue, 0, availableCount);

            recipeVM.SetCount(count);
            countText.SetTextParams(count);
        }
    }
}