using AD.ToolsCollection;

namespace Game.Instances
{
    public class InstanceScoresList : ListElement<InstanceScoreData, InstanceScoreItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Instance Scores";

            base.BindData(data);
        }
    }
}