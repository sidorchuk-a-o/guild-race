using AD.Services.Router;
using AD.ToolsCollection;

namespace Game.Instances
{
    [VMFactoryEditor(typeof(InstancesVMFactoryInstaller))]
    public class InstancesVMFactoryInstallerEditor : VMFactoryInstallerEditor
    {
        private ConsumableMechanicVMFactoriesList mechanicFactoriesList;
        private RewardVMFactoriesList rewardsFactoriesList;

        protected override void CreateElementGUI(Element root)
        {
            mechanicFactoriesList = root.CreateElement<ConsumableMechanicVMFactoriesList>();
            rewardsFactoriesList = root.CreateElement<RewardVMFactoriesList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            mechanicFactoriesList.BindProperty("mechanicFactories", data);
            rewardsFactoriesList.BindProperty("rewardsFactories", data);
        }
    }
}