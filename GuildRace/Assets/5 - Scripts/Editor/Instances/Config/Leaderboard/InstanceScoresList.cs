using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class InstanceScoresList : ListElement<InstanceScoreData>
    {
        protected override Type ItemType { get; } = typeof(InstanceScoreItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Instance Scores";
        }
    }
}