using System;
using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    public class EquipItemsList : ListElement<EquipItemData>
    {
        protected override Type ItemType { get; } = typeof(EquipItemItem);
        protected override Type WizardType { get; } = typeof(EquipItemImportWizard);

        private IntegerField minLevelFilter;
        private IntegerField maxLevelFilter;
        private KeyElement<int> typeFilter;

        protected override List<Header> Headers => new()
        {
            new("Icon", 68),
            new("Id", 70),
            new("Title"),
            new("Level", 60),
            new("Params (HP/AP)", 105),
            new("Type", 205),
            new("Slot", 180),
            new("Rarity", 140)
        };

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }

        protected override void CreateFilters(VisualElement root)
        {
            base.CreateFilters(root);

            var filterBlock = root.CreateRowContainer();
            var buttonsBlock = root.CreateRowContainer();

            filterBlock.TextAnchor(TextAnchor.MiddleLeft);
            buttonsBlock.TextAnchor(TextAnchor.MiddleLeft);

            // level

            filterBlock.Create<Label>().text = "Level:";

            minLevelFilter = filterBlock.Create<IntegerField>();
            minLevelFilter.Width(65);

            filterBlock.Create<Label>().text = "-";

            maxLevelFilter = filterBlock.Create<IntegerField>();
            maxLevelFilter.Width(65);

            // type

            var typeLabel = filterBlock.Create<Label>();
            typeLabel.PaddingLeft(15);
            typeLabel.text = "Type:";

            typeFilter = filterBlock.CreateKeyPopup<EquipType, int>();
            typeFilter.Width(225);
            typeFilter.FilterOn = false;
            typeFilter.UpdateOn = false;

            // buttons

            buttonsBlock.FlexGrow(0);
            buttonsBlock.CreateButton("Apply Filter", ApplyFilter);
            buttonsBlock.CreateButton("Reset Filter", ResetFilter);
        }

        private void ApplyFilter()
        {
            FilterMethod = x =>
            {
                var minLevel = minLevelFilter.value;
                var maxLevel = maxLevelFilter.value == 0 ? int.MaxValue : maxLevelFilter.value;
                var levelCheck = x.Level >= minLevel && x.Level <= maxLevel;

                var type = (EquipType)typeFilter.DisplayValue;
                var typeCheck = !type.IsValid || type == 0 || x.Type == type;

                return levelCheck && typeCheck;
            };
        }

        public override void ResetFilter()
        {
            base.ResetFilter();

            minLevelFilter.value = 0;
            maxLevelFilter.value = 0;
            typeFilter.ResetValue();
        }
    }
}