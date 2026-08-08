using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class CompleteChanceItem : ListItemElement<CompleteChanceData>
    {
        private PropertyElement instanceTypeField;
        private PropertyElement charactersCountModField;
        private PropertyElement resolveThreatModField;
        private PropertyElement maxResolveCountField;
        private UnitParamsChanceElement healthModEditor;
        private UnitParamsChanceElement powerModEditor;
        private ConsumableChancesList consumablesList;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            root.ConvertToColumn();
            instanceTypeField = root.CreateProperty();

            root.CreateHeader("Main");
            charactersCountModField = root.CreateProperty();
            resolveThreatModField = root.CreateProperty();
            maxResolveCountField = root.CreateProperty();

            root.CreateHeader("Health Params");
            healthModEditor = root.CreateElement<UnitParamsChanceElement>();

            root.CreateHeader("Power Params");
            powerModEditor = root.CreateElement<UnitParamsChanceElement>();

            root.CreateHeader("Consumables");
            consumablesList = root.CreateElement<ConsumableChancesList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            instanceTypeField.BindProperty("instanceType", data);
            charactersCountModField.BindProperty("charactersCountMod", data);
            resolveThreatModField.BindProperty("resolveThreatMod", data);
            maxResolveCountField.BindProperty("maxResolveCount", data);
            healthModEditor.BindProperty("healthMod", data);
            powerModEditor.BindProperty("powerMod", data);
            consumablesList.BindProperty("consumables", data);
        }
    }
}