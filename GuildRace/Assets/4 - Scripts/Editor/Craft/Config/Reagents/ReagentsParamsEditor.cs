using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Editor: <see cref="ReagentsParams"/>
    /// </summary>
    public class ReagentsParamsEditor
    {
        private ReagentItemsList itemsList;
        private GridParamsForItemsElement gridParamsEditor;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("reagentsParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Items", CreateItemsTab);
            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateItemsTab(VisualElement root, SerializedData data)
        {
            itemsList = root.CreateElement<ReagentItemsList>();
            itemsList.BindProperty("items", GetData(data));
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            gridParamsEditor = root.CreateElement<GridParamsForItemsElement>();
            gridParamsEditor.BindProperty("gridParams", GetData(data));
            gridParamsEditor.FlexWidth(33);
        }
    }
}