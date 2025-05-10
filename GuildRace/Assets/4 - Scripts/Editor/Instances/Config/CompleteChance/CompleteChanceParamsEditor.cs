using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Editor: <see cref="CompleteChanceParams"/>
    /// </summary>
    public class CompleteChanceParamsEditor
    {
        private PropertyElement guaranteedCompletedCountField;
        private CompleteChancesList completeChancesList;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("completeChanceParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Parameters", CreateParamsTab);
            tabs.content.FlexWidth(40);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Common");
            guaranteedCompletedCountField = root.CreateProperty();
            guaranteedCompletedCountField.BindProperty("guaranteedCompletedCount", GetData(data));

            root.CreateHeader("Parameters");
            completeChancesList = root.CreateElement<CompleteChancesList>();
            completeChancesList.BindProperty("parameters", GetData(data));
        }
    }
}