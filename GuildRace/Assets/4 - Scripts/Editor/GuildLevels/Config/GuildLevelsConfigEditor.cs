using AD.ToolsCollection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Game.GuildLevels
{
    [ConfigEditor(typeof(GuildLevelsConfig))]
    public class GuildLevelsConfigEditor : ConfigEditor
    {
        private LevelsList levelsList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Levels", CreateLevelsTab);
        }

        private void CreateLevelsTab(VisualElement root, SerializedData data)
        {
            levelsList = root.CreateElement<LevelsList>();
            levelsList.BindProperty("levels", data);
        }

        // == Menu ==

        [MenuItem("Game Services/Game/Guild Levels")]
        public static GuildLevelsConfigEditor GoToEditor()
        {
            return GoToEditor<GuildLevelsConfigEditor>(width: 500, height: 750);
        }

        public static GuildLevelsConfigEditor GoToEditor(GuildLevelsConfig config)
        {
            return GoToEditor<GuildLevelsConfigEditor>(config, width: 500, height: 750);
        }
    }
}