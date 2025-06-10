using AD.Services.Router;
using AD.ToolsCollection;

namespace Game.Instances
{
    [VMFactoryEditor(typeof(InstancesVMFactoryInstaller))]
    public class InstancesVMFactoryInstallerEditor : VMFactoryInstallerEditor
    {
        private ConsumableMechanicVMFactoriesList mechanicFactoriesList;

        protected override void CreateElementGUI(Element root)
        {
            mechanicFactoriesList = root.CreateElement<ConsumableMechanicVMFactoriesList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            mechanicFactoriesList.BindProperty("mechanicFactories", data);
        }
    }
}