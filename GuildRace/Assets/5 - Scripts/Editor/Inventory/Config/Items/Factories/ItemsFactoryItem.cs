using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsFactoryItem : ListItemElement<ItemsFactory>
    {
        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            TitleLabel.EditButtonOn = true;
        }
    }
}