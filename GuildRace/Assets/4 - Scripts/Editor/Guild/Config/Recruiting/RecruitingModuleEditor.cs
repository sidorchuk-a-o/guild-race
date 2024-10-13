using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Editor: <see cref="RecruitingModuleData"/>
    /// </summary>
    public class RecruitingModuleEditor
    {
        private PropertyElement minRequestCountField;
        private PropertyElement maxRequestCountField;
        private PropertyElement requestLifetimeField;
        private PropertyElement minNextRequestTimeField;
        private PropertyElement maxNextRequestTimeField;
        private PropertyElement weightSelectedRoleField;
        private PropertyElement weightUnselectedRoleField;
        private DefaultCharactersList defaultCharactersList;

        private static SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("recruitingModule");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Common", CreateCommonTab);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            minRequestCountField = root.CreateProperty();
            minRequestCountField.BindProperty("minRequestCount", GetData(data));

            maxRequestCountField = root.CreateProperty();
            maxRequestCountField.BindProperty("maxRequestCount", GetData(data));

            requestLifetimeField = root.CreateProperty();
            requestLifetimeField.BindProperty("requestLifetime", GetData(data));

            root.CreateSpace();

            minNextRequestTimeField = root.CreateProperty();
            minNextRequestTimeField.BindProperty("minNextRequestTime", GetData(data));

            maxNextRequestTimeField = root.CreateProperty();
            maxNextRequestTimeField.BindProperty("maxNextRequestTime", GetData(data));

            root.CreateSpace();

            weightSelectedRoleField = root.CreateProperty();
            weightSelectedRoleField.BindProperty("weightSelectedRole", GetData(data));

            weightUnselectedRoleField = root.CreateProperty();
            weightUnselectedRoleField.BindProperty("weightUnselectedRole", GetData(data));

            root.CreateHeader("Default Characters");

            defaultCharactersList = root.CreateElement<DefaultCharactersList>();
            defaultCharactersList.BindProperty("defaultCharacters", GetData(data));
        }
    }
}