using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [ConfigEditor(typeof(QuestsConfig))]
    public class QuestsConfigEditor : ConfigEditor
    {
        private QuestsList questsList;
        private QuestsGroupModulesList groupModulesList;
        private QuestMechanicHandlersList mechanicHandlersList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Quests", CreateQuestsTab);
            tabs.CreateTab("Group Modules", CreateGroupModulesTab);
            tabs.CreateTab("Mechanic Handlers", CreateMechanicHandlersTab);
        }

        private void CreateQuestsTab(VisualElement root, SerializedData data)
        {
            questsList = root.CreateElement<QuestsList>();
            questsList.BindProperty("quests", data);
        }

        private void CreateGroupModulesTab(VisualElement root, SerializedData data)
        {
            groupModulesList = root.CreateElement<QuestsGroupModulesList>();
            groupModulesList.BindProperty("groupModules", data);
        }

        private void CreateMechanicHandlersTab(VisualElement root, SerializedData data)
        {
            mechanicHandlersList = root.CreateElement<QuestMechanicHandlersList>();
            mechanicHandlersList.BindProperty("mechanicHandlers", data);
        }

        // == Menu ==

        [MenuItem("Game Services/Game/Quests")]
        public static QuestsConfigEditor GoToEditor()
        {
            return GoToEditor<QuestsConfigEditor>();
        }

        public static QuestsConfigEditor GoToEditor(QuestsConfig config)
        {
            return GoToEditor<QuestsConfigEditor>(config);
        }
    }
}