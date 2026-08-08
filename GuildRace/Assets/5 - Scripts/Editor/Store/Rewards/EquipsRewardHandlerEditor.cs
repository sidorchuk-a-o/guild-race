using AD.Game.Instances;
using AD.ToolsCollection;
using UnityEngine.UIElements;
using RewardHandlerEditor = AD.Store.RewardHandlerEditor;

namespace AD.Game.Store
{
    [DataEditor(typeof(EquipsRewardHandler))]
    public class EquipsRewardHandlerEditor : RewardHandlerEditor
    {
        private PopupElement<int> mechanicIdPopup;

        protected override void CreateContentGUI(VisualElement root)
        {
            base.CreateContentGUI(root);

            mechanicIdPopup = root.CreatePopup(InstancesEditorState.CreateRewardMechanicsViewCollection);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            mechanicIdPopup.BindProperty("mechanicId", data);
        }
    }
}