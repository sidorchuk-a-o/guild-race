using AD.ToolsCollection;
using System;
using System.Linq;

namespace Game.GuildLevels
{
    public class GuildLevelsEditorState : EditorState<GuildLevelsEditorState>
    {
        private GuildLevelsConfig config;
        private GuildLevelsEditorsFactory editorsFactory;
        private LevelMechanicsFactory levelMechanicsFactory;

        public static GuildLevelsConfig Config => FindAsset(ref instance.config);
        public static GuildLevelsEditorsFactory EditorsFactory => instance.editorsFactory ??= new();
        public static LevelMechanicsFactory LevelMechanicsFactory => instance.levelMechanicsFactory ??= new();

        public static Collection<Type> GetGuildLevelMechanicsCollection()
        {
            var types = ReflectionEditorUtils.GetTypesDerivedFrom<LevelMechanic>();

            var options = types.Select(x =>
            {
                var data = LevelMechanicsFactory.GetEditorData(x);

                return data?.Title ?? x.Name;
            });

            return new Collection<Type>(types, options);
        }
    }
}