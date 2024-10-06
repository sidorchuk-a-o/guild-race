using AD.ToolsCollection;
using UnityEditor;

namespace Game.Guild
{
    [ConfigEditor(typeof(GuildConfig))]
    public class GuildConfigEditor : ConfigEditor
    {
        // == Menu ==

        [MenuItem("Game Services/Game/Guild")]
        public static GuildConfigEditor GoToEditor()
        {
            return GoToEditor<GuildConfigEditor>();
        }

        public static GuildConfigEditor GoToEditor(GuildConfig config)
        {
            return GoToEditor<GuildConfigEditor>(config);
        }
    }
}