using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [UnityEditor.CustomEditor(typeof(ItemSlotsUIParams))]
    public class ItemSlotsUIParamsEditor : MonoEditor
    {
        private ItemSlotUIParamsList slotsParamsList;

        public override void CreateGUI(VisualElement root)
        {
            slotsParamsList = root.CreateElement<ItemSlotUIParamsList>();
        }

        public override void BindData(SerializedData data)
        {
            slotsParamsList.BindProperty("slotsParams", data);
        }
    }
}