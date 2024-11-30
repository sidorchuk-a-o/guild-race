using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [Menu("Reagents")]
    [ItemSlotEditor(typeof(ReagentSlotData))]
    public class ReagentSlotEditor : ItemSlotEditor
    {
        protected override void CreateCommonTab(VisualElement root, SerializedData data)
        {
            base.CreateCommonTab(root, data);

            root.CreateHeader("Reagent");
        }
    }
}