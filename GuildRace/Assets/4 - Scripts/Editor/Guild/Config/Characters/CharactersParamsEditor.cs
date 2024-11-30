using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Editor: <see cref="CharactersParams"/>
    /// </summary>
    public class CharactersParamsEditor
    {
        private RolesList rolesList;
        private SubRolesList subRolesList;
        private ClassesList classesList;

        private static SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("charactersParams");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Common", CreateCommonTab);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Roles");

            rolesList = root.CreateElement<RolesList>();
            rolesList.BindProperty("roles", GetData(data));

            root.CreateHeader("Sub Roles");

            subRolesList = root.CreateElement<SubRolesList>();
            subRolesList.BindProperty("subRoles", GetData(data));

            root.CreateHeader("Classes");

            classesList = root.CreateElement<ClassesList>();
            classesList.BindProperty("classes", GetData(data));
        }
    }
}