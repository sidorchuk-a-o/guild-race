using System;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class CompleteChancesList : ListElement<CompleteChanceData>
    {
        protected override Type ItemType { get; } = typeof(CompleteChanceItem);
    }
}