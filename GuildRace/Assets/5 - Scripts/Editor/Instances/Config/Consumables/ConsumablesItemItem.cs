using System.Collections.Generic;
using System.Linq;
using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class ConsumablesItemItem : ListItemElement<ConsumablesItemData>
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
            iconField.LabelOn = false;
            iconField.NameOn = false;

            idLabel = root.CreateProperty();
            idLabel.Width(70).FontSize(16).ReadOnly();
            idLabel.LabelOn = false;

            titleLabel = root.CreateElement<LabelElement>();
            titleLabel.FlexGrow(1).FontSize(16).Height(100, LengthUnit.Percent);
            titleLabel.LabelOn = false;

            mechanicIdPopup = root.CreatePopup(InstancesEditorState.CreateConsumableMechanicsViewCollection);
            mechanicIdPopup.Width(200).FontSize(16).Overflow(Overflow.Hidden);
            mechanicIdPopup.LabelOn = false;

            mechanicParamsContainer = root.CreateContainer();
            mechanicParamsContainer.Width(200).FontSize(16).PaddingLeft(5).Overflow(Overflow.Hidden);

            typeField = root.CreateKeyPopup<ConsumableType, int>();
            typeField.Width(120).FontSize(16);
            typeField.LabelOn = false;
            typeField.RemoveOn = false;
            typeField.FilterOn = false;
            typeField.UpdateOn = false;

            rarityField = root.CreateKeyPopup<Rarity, int>();
            rarityField.Width(120).FontSize(16);
            rarityField.LabelOn = false;
            rarityField.RemoveOn = false;
            rarityField.FilterOn = false;
            rarityField.UpdateOn = false;

            stackField = root.CreateProperty();
            stackField.Width(70).FontSize(16);
            stackField.LabelOn = false;
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

            TitleLabel.Display(false);

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

            var config = InstancesEditorState.Config;
            var mechanicHandlers = config.ConsumablesParams.MechanicHandlers;
            var mechanicData = mechanicHandlers.FirstOrDefault(x => x.Id == mechanicId);

            if (mechanicData == null)
            {
                return;
            }

            var factory = DataEditorsFactory.Instance;
            var mechanicEditor = (ConsumableMechanicHandlerEditor)factory.CreateEditor(mechanicData.GetType());

            mechanicParamsContainer.Clear();

            mechanicEditor.CreateParamsEditor(mechanicParamsContainer, mechanicParams);
        }
    }
}