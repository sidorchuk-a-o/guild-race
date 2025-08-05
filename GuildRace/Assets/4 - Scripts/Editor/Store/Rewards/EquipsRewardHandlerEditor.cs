using AD.Services.Store;
using AD.ToolsCollection;
using Game.Instances;
using UnityEngine.UIElements;
using RewardHandlerEditor = AD.Services.Store.RewardHandlerEditor;

namespace Game.Store
{
    [StoreEditor(typeof(EquipsRewardHandler))]
    public class EquipsRewardHandlerEditor : RewardHandlerEditor
    {
        private PopupElement<int> mechanicIdPopup;

        protected override void CreateSimpleContentGUI(VisualElement root)
        {
            base.CreateSimpleContentGUI(root);

            mechanicIdPopup = root.CreatePopup(InstancesEditorState.CreateRewardMechanicsViewCollection, false);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            mechanicIdPopup.BindProperty("mechanicId", data);
        }
    }
}