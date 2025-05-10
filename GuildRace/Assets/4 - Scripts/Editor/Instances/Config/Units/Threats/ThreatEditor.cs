using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(ThreatData))]
    public class ThreatEditor : EntityEditor
    {
        private PropertyElement nameKeyField;
        private PropertyElement descKeyField;
        private AddressableElement<Sprite> iconRefField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
        }

        private void CreateParamsTab(VisualElement root, SerializedData data)
        {
            root.CreateHeader("View");

            nameKeyField = root.CreateProperty();
            nameKeyField.BindProperty("nameKey", data);

            descKeyField = root.CreateProperty();
            descKeyField.BindProperty("descKey", data);

            iconRefField = root.CreateAddressable<Sprite>();
            iconRefField.BindProperty("iconRef", data);
        }
    }
}