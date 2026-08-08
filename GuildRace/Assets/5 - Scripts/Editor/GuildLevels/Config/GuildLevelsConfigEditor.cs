using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.GuildLevels
{
    public class GuildLevelsConfigEditor : ConfigEditor<GuildLevelsConfig>
    {
        private LevelsList levelsList;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Levels", CreateLevelsTab);
        }

        private void CreateLevelsTab(VisualElement root, SerializedData data)
        {
            levelsList = root.CreateElement<LevelsList>();
            levelsList.BindProperty("levels", data);
        }
    }
}