using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(DefaultCharacterData))]
    public class DefaultCharacterEditor : EntityEditor
    {
        private KeyElement classIdKey;
        private KeyElement specIdKey;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Common", CreateCommonTab);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            classIdKey = root.CreateKey<ClassId>();
            classIdKey.BindProperty("classId", data);

            specIdKey = root.CreateKey<SpecializationId>();
            specIdKey.BindProperty("specId", data);
        }
    }
}