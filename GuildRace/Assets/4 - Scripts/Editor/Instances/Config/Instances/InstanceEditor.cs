using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(InstanceData))]
    public class InstanceEditor : EntityEditor
    {
        private PropertyElement typeField;
        private PropertyElement nameKeyField;
        private PropertyElement descKeyField;
        private AddressableElement<Sprite> imageRefField;

        private UnitsList boosUnitsList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
            tabs.content.Width(50, LengthUnit.Percent);
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

            root.CreateHeader("Boos Units");

            boosUnitsList = root.CreateElement<UnitsList>();
            boosUnitsList.BindProperty("boosUnits", data);
            boosUnitsList.headerTitle = string.Empty;
        }
    }
}