using System;
using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class SquadsList : ListElement<SquadData>
    {
        protected override Type ItemType { get; } = typeof(SquadItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Squads";

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;

            UpdateData(data);
        }

        private static void UpdateData(SerializedData data)
        {
            var squads = data.GetValue<List<SquadData>>(new());
            var instanceTypes = InstancesEditorState.Config.InstanceTypes;

            // remove old
            var removedCount = squads.RemoveAll(squad =>
            {
                var checkResult = instanceTypes.Any(type =>
                {
                    return type.Id == squad.InstanceType;
                });

                return !checkResult;
            });

            if (removedCount > 0)
            {
                data.MarkAsDirty();
            }

            // create new
            foreach (var instanceType in instanceTypes)
            {
                if (squads.Any(x => x.InstanceType == instanceType.Id))
                {
                    continue;
                }

                squads.Add(new(instanceType.Id));

                data.MarkAsDirty();
            }
        }
    }
}