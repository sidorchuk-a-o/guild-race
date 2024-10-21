using AD.ToolsCollection;

namespace Game.Items
{
    /// <summary>
    /// Item: <see cref="EquipGroupData"/>
    /// </summary>
    public class EquipGroupItem : EntityListItemElement
    {
        protected override IEditorsFactory EditorsFactory => ItemsEditorState.EditorsFactory;

        public override void BindData(SerializedData data)
        {
            foldoutOn = true;

            base.BindData(data);
        }
    }
}