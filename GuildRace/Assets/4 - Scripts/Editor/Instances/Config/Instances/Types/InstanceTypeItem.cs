﻿using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="InstanceTypeData"/>
    /// </summary>
    public class InstanceTypeItem : EntityListItemElement
    {
        private ItemIdElement idLabel;

        protected override IEditorsFactory EditorsFactory => InstancesEditorState.EditorsFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            idLabel = root.CreateElement<ItemIdElement>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindData(data);
        }
    }
}