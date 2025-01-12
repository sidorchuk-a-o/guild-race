using AD.ToolsCollection;
using Game.Guild;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    public class SquadRolesList : ListElement<SquadRoleData, SquadRoleItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Squad Roles";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            UpdateData(data);

            base.BindData(data);
        }

        private void UpdateData(SerializedData data)
        {
            var squadRoles = data.GetValue<List<SquadRoleData>>(new());
            var roles = GuildEditorState.Config.CharactersParams.Roles;

            // remove old
            var removedCount = squadRoles.RemoveAll(squadRole =>
            {
                var checkResult = roles.Any(role =>
                {
                    return role.Id == squadRole.Role;
                });

                return checkResult == false;
            });

            if (removedCount > 0)
            {
                data.MarkAsDirty();
            }

            // create new
            foreach (var role in roles)
            {
                if (squadRoles.Any(x => x.Role == role.Id))
                {
                    continue;
                }

                squadRoles.Add(new SquadRoleData(role.Id));

                data.MarkAsDirty();
            }
        }
    }
}