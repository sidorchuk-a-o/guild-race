using AD.ToolsCollection;

namespace Game.Craft
{
    public class CraftEditorState : EditorState<CraftEditorState>
    {
        private CraftConfig config;
        private CraftEditorsFactory editorsFactory;

        public static CraftConfig Config => FindAsset(ref instance.config);
        public static CraftEditorsFactory EditorsFactory => instance.editorsFactory ??= new();

        public static Collection<int> GetReagentsCollection()
        {
            return Config.CreateKeyCollection<ReagentItemData, int>("reagentsParams.items");
        }
    }
}