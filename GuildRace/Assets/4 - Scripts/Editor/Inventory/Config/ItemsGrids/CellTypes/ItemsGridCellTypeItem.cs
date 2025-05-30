﻿using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="ItemsGridCellTypeData"/>
    /// </summary>
    public class ItemsGridCellTypeItem : EntityListItemElement
    {
        private ItemIdElement idLabel;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            idLabel = root.CreateElement<ItemIdElement>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindData(data);

            titleLabel.editButtonOn = true;
        }
    }
}