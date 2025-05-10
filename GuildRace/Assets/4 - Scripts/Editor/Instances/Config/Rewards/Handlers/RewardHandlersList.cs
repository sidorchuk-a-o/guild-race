using AD.ToolsCollection;

namespace Game.Instances
{
    public class RewardHandlersList : ListElement<RewardHandler, RewardHandlerItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Reward Mechanics";
            wizardType = typeof(RewardHandlerImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}