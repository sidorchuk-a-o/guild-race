using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class GuildEditorState : EditorState<GuildEditorState>
    {
        private GuildConfig config;

        public static GuildConfig Config => FindAsset(ref Instance.config);

        public static Collection<int> CreateRolesViewCollection()
        {
            return Config.CreateKeyViewCollection<RoleData, int>("charactersParams.roles");
        }

        public static Collection<int> CreateSubRolesViewCollection()
        {
            return Config.CreateKeyViewCollection<SubRoleData, int>("charactersParams.subRoles");
        }

        public static Collection<int> CreateClassesViewCollection()
        {
            return Config.CreateKeyViewCollection<ClassData, int>("charactersParams.classes");
        }

        public static Collection<int> CreateSpecializationsViewCollection()
        {
            var keysDict = new Dictionary<string, int> { ["< null >"] = -1 };
            var classes = Config.GetValue<List<ClassData>>("charactersParams.classes");

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