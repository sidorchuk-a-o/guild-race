﻿using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    public class ItemSlotKeyItem : ListItemElement
    {
        private KeyElement keyField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            keyField = root.CreateKey<ItemSlot>();
            keyField.FlexGrow(1);
            keyField.removeOn = false;
            keyField.filterOn = false;
            keyField.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            keyField.BindData(data);
        }
    }
}