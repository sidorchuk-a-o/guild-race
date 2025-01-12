using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [Menu("Сonsumables")]
    [ItemSlotEditor(typeof(СonsumablesSlotData))]
    public class СonsumablesSlotEditor : ItemSlotEditor
    {
        protected override void CreateCommonTab(VisualElement root, SerializedData data)
        {
            base.CreateCommonTab(root, data);

            root.CreateHeader("Сonsumables");
        }
    }
}