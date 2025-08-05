using AD.Services.Store;
using AD.ToolsCollection;

namespace Game.Store
{
    [Hidden, Menu("Equips Reward")]
    [RewardsSetEditor(typeof(EquipsReward))]
    public class EquipsRewardEditor : RewardEditor
    {
        private PropertyElement countField;
        private PropertyElement instanceTypeField;
        private PropertyElement previewKey;

        protected override void CreateElementGUI(Element root)
        {
            countField = root.CreateProperty();
            instanceTypeField = root.CreateProperty();

            root.CreateSpace();

            previewKey = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            countField.BindProperty("count", data);
            instanceTypeField.BindProperty("instanceType", data);
            previewKey.BindProperty("previewKey", data);
        }
    }
}