using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(InstanceData))]
    public class InstanceEditor : DataEditor
    {
        private PropertyElement typeField;
        private PropertyElement nameKeyField;
        private PropertyElement descKeyField;
        private AddressableElement<Sprite> imageRefField;
        private AddressableElement<Sprite> loadingRefField;

        private UnitsList boosUnitsList;

        protected override void CreateTabsGUI(TabsContainer tabs)
        {
            base.CreateTabsGUI(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
            tabs.Content.Width(50, LengthUnit.Percent);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("Params");

            typeField = root.CreateProperty();
            typeField.BindProperty("type", data);

            root.CreateHeader("View");

            nameKeyField = root.CreateProperty();
            nameKeyField.BindProperty("nameKey", data);

            descKeyField = root.CreateProperty();
            descKeyField.BindProperty("descKey", data);

            imageRefField = root.CreateAddressable<Sprite>();
            imageRefField.BindProperty("imageRef", data);

            loadingRefField = root.CreateAddressable<Sprite>();
            loadingRefField.BindProperty("loadingRef", data);

            root.CreateHeader("Boos Units");

            boosUnitsList = root.CreateElement<UnitsList>();
            boosUnitsList.BindProperty("boosUnits", data);
            boosUnitsList.HeaderTitle = string.Empty;
        }
    }
}