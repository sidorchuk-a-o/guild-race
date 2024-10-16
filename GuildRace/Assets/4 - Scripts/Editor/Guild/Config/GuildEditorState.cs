using AD.ToolsCollection;
using System.Collections.Generic;

namespace Game.Guild
{
    public class GuildEditorState : EditorState<GuildEditorState>
    {
        private GuildConfig config;
        private GuildEditorsFactory editorsFactory;

        public static GuildConfig Config => FindAsset(ref instance.config);
        public static GuildEditorsFactory EditorsFactory => instance.editorsFactory ??= new();

        public static Collection<string> CreateRolesViewCollection()
        {
            return Config.CreateKeyViewCollection<RoleData>("charactersModule.roles");
        }

        public static Collection<string> CreateClassesViewCollection()
        {
            return Config.CreateKeyViewCollection<ClassData>("charactersModule.classes");
        }

        public static Collection<string> CreateSpecializationsViewCollection()
        {
            var keysDict = new Dictionary<string, string> { ["< null >"] = null };
            var classes = Config.GetValue<List<ClassData>>("charactersModule.classes");

            foreach (var classData in classes)
            {
                var classKey = classData.Title;

                foreach (var spec in classData.Specs)
                {
                    var specKey = $"{classKey}/{spec.Title}";
                    var specId = spec.Id;

                    keysDict.Add(specKey, specId);
                }
            }

            return new(keysDict.Values, keysDict.Keys, autoSort: false);
        }
    }
}