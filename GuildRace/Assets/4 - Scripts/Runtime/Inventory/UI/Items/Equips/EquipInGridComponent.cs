﻿using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class EquipInGridComponent : ItemInGridComponent
    {
        [Header("Equip")]
        [SerializeField] private Image rarityImage;

        public override void Init(ItemVM itemVM, CompositeDisp disp)
        {
            base.Init(itemVM, disp);

            var equipVM = itemVM as EquipItemVM;

            rarityImage.color = equipVM.RarityVM.Color;
        }
    }
}