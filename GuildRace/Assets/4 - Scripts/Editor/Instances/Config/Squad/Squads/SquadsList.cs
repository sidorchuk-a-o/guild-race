using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    public class SquadsList : ListElement<SquadData, SquadItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Squads";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            UpdateData(data);

            base.BindData(data);
        }

        private void UpdateData(SerializedData data)
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

                return checkResult == false;
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

                squads.Add(new SquadData(instanceType.Id));

                data.MarkAsDirty();
            }
        }
    }
}