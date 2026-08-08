using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class AbilitiesList : ListElement<AbilityData>
    {
        protected override Type ItemType { get; } = typeof(AbilityItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Abilities";

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}