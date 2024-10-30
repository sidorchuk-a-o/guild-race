using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [Menu("Equips")]
    [ItemSlotEditor(typeof(EquipSlotData))]
    public class EquipSlotEditor : ItemSlotEditor
    {
        private PropertyElement equipGroupField;

        protected override void CreateCommonTab(VisualElement root, SerializedData data)
        {
            base.CreateCommonTab(root, data);

            root.CreateHeader("Equip");

            equipGroupField = root.CreateProperty();
            equipGroupField.BindProperty("equipGroup", data);
        }
    }
}