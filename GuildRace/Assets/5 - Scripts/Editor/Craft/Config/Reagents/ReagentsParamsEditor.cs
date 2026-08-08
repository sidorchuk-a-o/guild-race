using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Craft
{
    /// <summary>
    ///     Editor: <see cref="ReagentsParams" />
    /// </summary>
    public class ReagentsParamsEditor
    {
        private ReagentItemsList itemsList;
        private GridParamsForItemsElement gridParamsEditor;

        private static SerializedData GetData(SerializedData data)
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
            itemsList.FlexWidth(50);
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