using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(DefaultCharacterData))]
    public class DefaultCharacterEditor : EntityEditor
    {
        private KeyElement<string> classIdKey;
        private KeyElement<string> specIdKey;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Common", CreateCommonTab);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            classIdKey = root.CreateKey<ClassId, string>();
            classIdKey.BindProperty("classId", data);

            specIdKey = root.CreateKey<SpecializationId, string>();
            specIdKey.BindProperty("specId", data);
        }
    }
}