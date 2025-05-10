using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(AbilityData))]
    public class AbilityEditor : EntityEditor
    {
        private PropertyElement nameKeyField;
        private PropertyElement descKeyField;
        private PropertyElement threatIdField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = root.CreateProperty();
            nameKeyField.BindProperty("nameKey", data);

            descKeyField = root.CreateProperty();
            descKeyField.BindProperty("descKey", data);

            threatIdField = root.CreateProperty();
            threatIdField.BindProperty("threatId", data);
        }
    }
}