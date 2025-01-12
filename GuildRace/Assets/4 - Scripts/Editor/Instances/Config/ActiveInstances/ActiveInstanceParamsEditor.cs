using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Editor: <see cref="ActiveInstanceParams"/>
    /// </summary>
    public class ActiveInstanceParamsEditor
    {
        private PropertyElement tempCompeteTimeField;

        private SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("activeInstanceParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Active Instance Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            tempCompeteTimeField = root.CreateProperty();
            tempCompeteTimeField.FlexGrow(1).MaxWidth(33, LengthUnit.Percent).MarginRight(10);
            tempCompeteTimeField.BindProperty("tempCompeteTime", GetData(data));
        }
    }
}