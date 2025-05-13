using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Editor: <see cref="RecyclingParams"/>
    /// </summary>
    public class RecyclingParamsEditor
    {
        private RecycleSlotElement recycleSlotEditor;
        private RecyclingItemsList recyclingItemsList;
        private RecyclingReagentsList recyclingReagentsList;
        private ItemByIdList ignoreReagentsList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("recyclingParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Params", CreateParamsTab);
            tabs.content.FlexWidth(30);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Recycle Slot - Created");

            recycleSlotEditor = root.CreateElement<RecycleSlotElement>();
            recycleSlotEditor.BindProperty("recycleSlot", GetData(data));

            root.CreateHeader("Recycling Items");

            recyclingItemsList = root.CreateElement<RecyclingItemsList>();
            recyclingItemsList.BindProperty("recyclingItems", GetData(data));

            root.CreateHeader("Recycling Reagents");

            recyclingReagentsList = root.CreateElement<RecyclingReagentsList>();
            recyclingReagentsList.BindProperty("recyclingReagents", GetData(data));

            root.CreateHeader("Ignore Reagents");

            ignoreReagentsList = root.CreateElement<ItemByIdList>();
            ignoreReagentsList.BindProperty("ignoreReagents", GetData(data));
        }
    }
}