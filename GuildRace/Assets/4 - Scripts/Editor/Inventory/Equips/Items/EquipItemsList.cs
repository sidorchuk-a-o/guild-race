using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    public class EquipItemsList : ListElement<EquipItemData, EquipItemItem>
    {
        private IntegerField minLevelFilter;
        private IntegerField maxLevelFilter;
        private KeyElement typeFilter;

        protected override List<Header> Headers => new()
        {
            new Header("Icon", 68, LengthUnit.Pixel),
            new Header("Level", 75, LengthUnit.Pixel),
            new Header("Power", 125, LengthUnit.Pixel),
            new Header("Type", 207, LengthUnit.Pixel),
            new Header("Slot", 180, LengthUnit.Pixel),
            new Header("Rarity", 127, LengthUnit.Pixel),
            new Header("Source")
        };

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

            typeFilter = filterBlock.CreateKey<EquipType>();
            typeFilter.Width(225);
            typeFilter.filterOn = false;
            typeFilter.updateOn = false;

            // buttons

            buttonsBlock.FlexGrow(0);
            buttonsBlock.CreateButton(title: "Apply Filter", clickEvent: ApplyFilter);
            buttonsBlock.CreateButton(title: "Reset Filter", clickEvent: ResetFilter);
        }

        private void ApplyFilter()
        {
            SetFilterMethod(x =>
            {
                var minLevel = minLevelFilter.value;
                var maxLevel = maxLevelFilter.value == 0 ? int.MaxValue : maxLevelFilter.value;
                var levelCheck = x.Level >= minLevel && x.Level <= maxLevel;

                var type = (EquipType)typeFilter.DisplayValue;
                var typeCheck = type.IsValid == false || x.Type == type;

                return levelCheck && typeCheck;
            });
        }

        public override void ResetFilter()
        {
            base.ResetFilter();

            minLevelFilter.value = 0;
            maxLevelFilter.value = 0;
            typeFilter.ResetValue();
        }

        public override void BindData(SerializedData data)
        {
            wizardType = typeof(EquipItemCreateWizard);

            showCloneButton = false;

            base.BindData(data);

            ApplyFilter();
        }
    }
}