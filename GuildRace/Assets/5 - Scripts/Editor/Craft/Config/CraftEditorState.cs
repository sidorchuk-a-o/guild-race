using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class CraftEditorState : EditorState<CraftEditorState>
    {
        private CraftConfig config;

        public static CraftConfig Config => FindAsset(ref Instance.config);

        public static Collection<int> GetReagentsCollection()
        {
            return Config.CreateKeyCollection<ReagentItemData, int>("reagentsParams.items");
        }
    }
}