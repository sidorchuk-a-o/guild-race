using AD.ToolsCollection;

namespace Game.Craft
{
    public class RecyclingRarityModsList : ListElement<RecyclingRarityModData, RecyclingRarityModItem>
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            base.BindData(data);
        }
    }
}