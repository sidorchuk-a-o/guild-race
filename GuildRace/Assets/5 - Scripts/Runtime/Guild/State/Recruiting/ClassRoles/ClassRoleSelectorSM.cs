using Newtonsoft.Json;

namespace AD.Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class ClassRoleSelectorSM
    {
        [ES3Serializable] private int roleId;
        [ES3Serializable] private bool isEnabled;

        public ClassRoleSelectorSM(ClassRoleSelectorInfo info)
        {
            roleId = info.RoleId;
            isEnabled = info.IsEnabled.CurrentValue;
        }

        public ClassRoleSelectorInfo GetValue()
        {
            return new(roleId, isEnabled);
        }
    }
}