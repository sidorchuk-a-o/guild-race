using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    /// <summary>
    /// Editor: <see cref="CharactersModuleData"/>
    /// </summary>
    public class CharactersModuleEditor
    {
        private PropertyElement maxEquipSlotCountField;
        private RolesList rolesList;

        private ClassesList classesList;

        private static SerializedData GetData(SerializedData data)
        {
            return data.GetProperty("charactersModule");
        }

        public void CreateTabs(TabsContainer tabs)
        {
            tabs.CreateTab("Common", CreateCommonTab);
            tabs.CreateTab("Classes", CreateClassesTab);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Equips");

            maxEquipSlotCountField = root.CreateProperty();
            maxEquipSlotCountField.BindProperty("maxEquipSlotCount", GetData(data));

            root.CreateHeader("Roles");

            rolesList = root.CreateElement<RolesList>();
            rolesList.BindProperty("roles", GetData(data));
        }

        private void CreateClassesTab(VisualElement root, SerializedData data)
        {
            classesList = root.CreateElement<ClassesList>();
            classesList.BindProperty("classes", GetData(data));
        }
    }
}