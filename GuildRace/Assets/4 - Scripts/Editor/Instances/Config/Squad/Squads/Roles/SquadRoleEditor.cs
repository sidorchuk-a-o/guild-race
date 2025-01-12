using AD.ToolsCollection;

namespace Game.Instances
{
    [InstancesEditor(typeof(SquadRoleData))]
    public class SquadRoleEditor : Element
    {
        private PropertyElement maxUnitsCountField;

        protected override void CreateElementGUI(Element root)
        {
            maxUnitsCountField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            maxUnitsCountField.BindProperty("maxUnitsCount", data);
        }
    }
}