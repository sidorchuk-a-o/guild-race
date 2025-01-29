using AD.ToolsCollection;

namespace Game.Quests
{
    public class QuestsEditorState : EditorState<QuestsEditorState>
    {
        private QuestsConfig config;
        private QuestsEditorsFactory editorsFactory;

        public static QuestsConfig Config => FindAsset(ref instance.config);
        public static QuestsEditorsFactory EditorsFactory => instance.editorsFactory ??= new();

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