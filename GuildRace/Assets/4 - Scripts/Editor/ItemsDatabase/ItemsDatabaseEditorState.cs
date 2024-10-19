using AD.ToolsCollection;
using System.Collections.Generic;

namespace Game.Items
{
    public class ItemsDatabaseEditorState : EditorState<ItemsDatabaseEditorState>
    {
        private ItemsDatabaseConfig config;
        private ItemsEditorsFactory editorsFactory;

        public static ItemsDatabaseConfig Config => FindAsset(ref instance.config);

        public static ItemsEditorsFactory EditorsFactory => instance.editorsFactory ??= new();

        public static Collection<string> GetRaritiesCollection()
        {
            return Config.CreateKeyViewCollection<RarityData>("equipsParams.rarities");
        }

        public static Collection<string> GetEquipSlotsCollection()
        {
            return Config.CreateKeyViewCollection<EquipSlotData>("equipsParams.slots");
        }

        public static Collection<string> GetEquipGroupsCollection()
        {
            return Config.CreateKeyViewCollection<EquipGroupData>("equipsParams.groups");
        }

        public static Collection<string> GetEquipTypesCollection()
        {
            var keysDict = new Dictionary<string, string>();
            var equipGroups = Config.GetValue<List<EquipGroupData>>("equipsParams.groups");

            foreach (var equipGroup in equipGroups)
            {
                var groupKey = equipGroup.Title;

                foreach (var equipType in equipGroup.Types)
                {
                    var typeKey = $"{groupKey}/{equipType.Title}";

                    keysDict[typeKey] = equipType.Id;
                }
            }

            return new Collection<string>(keysDict.Values, keysDict.Keys, autoSort: false);
        }
    }
}