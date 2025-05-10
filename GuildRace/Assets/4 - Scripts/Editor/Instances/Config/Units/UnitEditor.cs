using AD.ToolsCollection;
using Game.Guild;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(UnitData))]
    public class UnitEditor : EntityEditor
    {
        private PropertyElement nameKeyField;
        private PropertyElement descKeyField;
        private AddressableElement<Sprite> imageRefField;
        private PropertyElement completeTimeField;
        private UnitParamsElement unitParamsField;
        private AbilitiesList abilitiesList;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            base.CreateTabItems(tabs);

            tabs.CreateTab("Params", CreateParamsTab);
            tabs.content.Width(50, LengthUnit.Percent);
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