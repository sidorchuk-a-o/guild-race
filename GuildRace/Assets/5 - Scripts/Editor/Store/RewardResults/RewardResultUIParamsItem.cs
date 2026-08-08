using System;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Store
{
    public class RewardResultUIParamsItem : ListItemElement<RewardResultUIParams>
    {
        private LabelElement typeField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            typeField = root.CreateElement<LabelElement>();
            typeField.LabelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            FoldoutOn = true;

            typeField.Text = Type.GetType(data.GetProperty("rewardType").GetValue<string>())!.Name;
        }
    }
}