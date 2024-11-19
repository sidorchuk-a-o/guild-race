using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class ClassRoleSelectorSM
    {
        [ES3Serializable] private int roleId;
        [ES3Serializable] private bool isEnabled;

        public ClassRoleSelectorSM(ClassRoleSelectorInfo info)
        {
            roleId = info.RoleId;
            isEnabled = info.IsEnabled.Value;
        }

        public ClassRoleSelectorInfo GetValue()
        {
            return new ClassRoleSelectorInfo(roleId, isEnabled);
        }
    }
}