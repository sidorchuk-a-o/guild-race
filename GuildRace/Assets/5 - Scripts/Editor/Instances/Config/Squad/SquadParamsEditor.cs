using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    /// <summary>
    ///     Editor: <see cref="SquadParams" />
    /// </summary>
    public class SquadParamsEditor
    {
        private ItemsGridElement<UnitBagData> bagEditor;
        private SquadsList squadsList;

        private static SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("squadParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Squad Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.ConvertToRow();

            squadsList = root.CreateElement<SquadsList>();
            squadsList.FlexWidth(33).MarginRight(10);
            squadsList.BindProperty("squads", GetData(data));

            bagEditor = root.CreateElement<ItemsGridElement<UnitBagData>>();
            bagEditor.FlexWidth(33).MarginRight(10);
            bagEditor.BindProperty("bag", GetData(data));
            bagEditor.Label = "Unit Bag Params";
        }
    }
}