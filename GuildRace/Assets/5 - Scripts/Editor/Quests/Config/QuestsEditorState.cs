using AD.ToolsCollection;

namespace AD.Game.Quests
{
    public class QuestsEditorState : EditorState<QuestsEditorState>
    {
        private QuestsConfig config;

        public static QuestsConfig Config => FindAsset(ref Instance.config);

        public static Collection<int> CreateQuestsGroupViewCollection()
        {
            return Config.CreateKeyViewCollection<QuestsGroupModule, int>("groupModules");
        }

        public static Collection<int> CreateQuestMechanicsViewCollection()
        {
            return Config.CreateKeyViewCollection<QuestMechanicHandler, int>("mechanicHandlers");
        }
    }
}