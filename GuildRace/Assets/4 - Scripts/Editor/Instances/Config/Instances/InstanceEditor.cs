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

        private AddressableElement<GameObject> mapRefField;
        private AddressableElement<GameObject> uiRefField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
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

            root.CreateHeader("Map Logic");

            mapRefField = root.CreateAddressable<GameObject>();
            mapRefField.BindProperty("mapRef", data);

            uiRefField = root.CreateAddressable<GameObject>();
            uiRefField.BindProperty("uiRef", data);

            root.CreateHeader("Bosses");
            root.CreateHeader("--- --- ---");

            root.CreateHeader("Trash Units");
            root.CreateHeader("--- --- ---");
        }
    }
}