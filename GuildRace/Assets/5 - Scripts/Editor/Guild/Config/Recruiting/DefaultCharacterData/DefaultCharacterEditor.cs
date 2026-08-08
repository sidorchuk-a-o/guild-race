using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    [DataEditor(typeof(DefaultCharacterData))]
    public class DefaultCharacterEditor : DataEditor
    {
        private PropertyElement classIdKey;
        private PropertyElement specIdKey;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

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

            Title = DefaultCharacterItem.GetTitle(data);
        }
    }
}