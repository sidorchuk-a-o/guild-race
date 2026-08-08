using System;
using System.Linq;
using AD.ToolsCollection;

namespace AD.Game.GuildLevels
{
    public class GuildLevelsEditorState : EditorState<GuildLevelsEditorState>
    {
        private GuildLevelsConfig config;

        public static GuildLevelsConfig Config => FindAsset(ref Instance.config);

        public static Collection<Type> GetGuildLevelMechanicsCollection()
        {
            var types = ReflectionEditorUtils.GetTypesDerivedFrom<LevelMechanic>();

            var options = types.Select(x =>
            {
                var data = DataEditorsFactory.Instance.GetEditorData(x);

                return data?.Title ?? x.Name;
            });

            return new(types, options);
        }
    }
}