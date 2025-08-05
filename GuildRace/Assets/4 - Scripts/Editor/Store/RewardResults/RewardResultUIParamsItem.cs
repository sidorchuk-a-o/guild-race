using System;
using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Store
{
    /// <summary>
    /// Item: <see cref="RewardResultUIParams"/>
    /// </summary>
    public class RewardResultUIParamsItem : ListItemElement
    {
        private LabelElement typeField;

        protected override IEditorsFactory EditorsFactory => StoreConfigState.EditorsFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            typeField = root.CreateElement<LabelElement>();
            typeField.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            foldoutOn = true;

            base.BindData(data);

            typeField.text = Type.GetType(data.GetProperty("rewardType").GetValue<string>()).Name;
        }
    }
}