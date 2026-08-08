using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Quests
{
    public class QuestsConfigEditor : ConfigEditor<QuestsConfig>
    {
        private QuestsList questsList;
        private QuestsGroupModulesList groupModulesList;
        private QuestMechanicHandlersList mechanicHandlersList;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Quests", CreateQuestsTab);
            tabs.CreateTab("Group Modules", CreateGroupModulesTab);
            tabs.CreateTab("Mechanic Handlers", CreateMechanicHandlersTab);
        }

        private void CreateQuestsTab(VisualElement root, SerializedData data)
        {
            questsList = root.CreateElement<QuestsList>();
            questsList.BindProperty("quests", data);
            questsList.FlexShrink(1);
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
    }
}