using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="ConsumablesItemData"/>
    /// </summary>
    public class ConsumablesItemItem : ListItemElement
    {
        private SpriteField iconField;
        private PropertyElement idLabel;
        private LabelElement titleLabel;
        private PopupElement<int> mechanicIdPopup;
        private VisualElement mechanicParamsContainer;
        private KeyElement<int> typeField;
        private KeyElement<int> rarityField;
        private PropertyElement stackField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();
            root.AlignItems(Align.Center);

            iconField = root.CreateElement<SpriteField>();
            iconField.SetSize(35);
            iconField.labelOn = false;
            iconField.nameOn = false;

            idLabel = root.CreateProperty();
            idLabel.Width(70).FontSize(16).ReadOnly();
            idLabel.labelOn = false;

            titleLabel = root.CreateElement<LabelElement>();
            titleLabel.FlexGrow(1).FontSize(16).Height(100, LengthUnit.Percent);
            titleLabel.labelOn = false;

            mechanicIdPopup = root.CreatePopup(InstancesEditorState.CreateConsumableMechanicsViewCollection, false);
            mechanicIdPopup.Width(200).FontSize(16).Overflow(Overflow.Hidden);
            mechanicIdPopup.labelOn = false;

            mechanicParamsContainer = root.CreateContainer();
            mechanicParamsContainer.Width(200).FontSize(16).PaddingLeft(5).Overflow(Overflow.Hidden);

            typeField = root.CreateKey<ConsumableType, int>();
            typeField.Width(120).FontSize(16);
            typeField.labelOn = false;
            typeField.removeOn = false;
            typeField.filterOn = false;
            typeField.updateOn = false;

            rarityField = root.CreateKey<Rarity, int>();
            rarityField.Width(120).FontSize(16);
            rarityField.labelOn = false;
            rarityField.removeOn = false;
            rarityField.filterOn = false;
            rarityField.updateOn = false;

            stackField = root.CreateProperty();
            stackField.Width(70).FontSize(16);
            stackField.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindProperty("id", data);
            titleLabel.BindProperty("title", data);
            iconField.BindProperty("iconRef", data);
            mechanicIdPopup.BindProperty("mechanicId", data);
            typeField.BindProperty("type", data);
            rarityField.BindProperty("rarity", data);
            stackField.BindProperty("size", data.GetProperty("stack"));

            BindMechanicParams(data);
        }

        private void BindMechanicParams(SerializedData data)
        {
            var mechanicId = data
                .GetProperty("mechanicId")
                .GetValue<int>();

            var mechanicParams = data
                .GetProperty("mechanicParams")
                .GetValue<List<string>>();

            var mechanicData = InstancesEditorState.Config.ConsumablesParams.MechanicHandlers.FirstOrDefault(x => x.Id == mechanicId);

            if (mechanicData == null)
            {
                return;
            }

            var mechanicEditor = InstancesEditorState.EditorsFactory.CreateEditor(mechanicData.GetType()) as ConsumableMechanicHandlerEditor;

            mechanicParamsContainer.Clear();
            mechanicEditor.CreateParamsEditor(mechanicParamsContainer, mechanicParams);
        }
    }
}