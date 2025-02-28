﻿using AD.ToolsCollection;
using AD.UI;
using UniRx;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemStackComponent : MonoBehaviour
    {
        [SerializeField] private bool showMaxValue;
        [SerializeField] private UIText sizeText;

        public ItemStackVM ViewModel { get; private set; }

        public void Init(ItemStackVM itemStackVM, CompositeDisp disp)
        {
            ViewModel = itemStackVM;

            itemStackVM.ObserveChanged()
                .Subscribe(UpdateSizeText)
                .AddTo(disp);

            UpdateSizeText();
        }

        private void UpdateSizeText()
        {
            sizeText.SetTextParams(showMaxValue
                ? $"{ViewModel.Value}/{ViewModel.Size}"
                : $"{ViewModel.Value}");

            this.SetActive(ViewModel.Value > 1);
        }
    }
}