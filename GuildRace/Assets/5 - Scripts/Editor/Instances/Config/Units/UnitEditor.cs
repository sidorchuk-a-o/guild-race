using AD.Game.Guild;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(UnitData))]
    public class UnitEditor : DataEditor
    {
        private PropertyElement nameKeyField;
        private PropertyElement descKeyField;
        private AddressableElement<Sprite> imageRefField;
        private PropertyElement completeTimeField;
        private UnitParamsElement unitParamsField;
        private AbilitiesList abilitiesList;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
            tabs.Content.Width(50, LengthUnit.Percent);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = root.CreateProperty();
            nameKeyField.BindProperty("nameKey", data);

            descKeyField = root.CreateProperty();
            descKeyField.BindProperty("descKey", data);

            imageRefField = root.CreateAddressable<Sprite>();
            imageRefField.BindProperty("imageRef", data);

            root.CreateHeader("Params");

            completeTimeField = root.CreateProperty();
            completeTimeField.BindProperty("completeTime", data);

            unitParamsField = root.CreateElement<UnitParamsElement>();
            unitParamsField.BindProperty("unitParams", data);

            root.CreateHeader("Abilities");

            abilitiesList = root.CreateElement<AbilitiesList>();
            abilitiesList.BindProperty("abilities", data);
        }
    }
}