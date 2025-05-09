using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Editor: <see cref="CompleteChanceParams"/>
    /// </summary>
    public class CompleteChanceParamsEditor
    {
        private CompleteChancesList completeChancesList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("completeChanceParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Parameters", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            completeChancesList = root.CreateElement<CompleteChancesList>();
            completeChancesList.BindProperty("parameters", GetData(data));
            completeChancesList.FlexWidth(40);
        }
    }
}