using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(DefaultCharacterData))]
    public class DefaultCharacterEditor : Editor
    {
        private PropertyElement classIdKey;
        private PropertyElement specIdKey;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Common", CreateCommonTab);
        }

        private void CreateCommonTab(VisualElement root, SerializedData data)
        {
            classIdKey = root.CreateProperty();
            classIdKey.BindProperty("classId", data);

            specIdKey = root.CreateProperty();
            specIdKey.BindProperty("specId", data);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            title = DefaultCharacterItem.GetTitle(data);
        }
    }
}