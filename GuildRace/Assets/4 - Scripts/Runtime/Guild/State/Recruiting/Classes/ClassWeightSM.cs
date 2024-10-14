using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class ClassWeightSM
    {
        [ES3Serializable] private string classId;
        [ES3Serializable] private bool isEnabled;

        public ClassWeightSM(ClassWeightInfo info)
        {
            classId = info.ClassId;
            isEnabled = info.IsEnabled.Value;
        }

        public ClassWeightInfo GetValue()
        {
            return new ClassWeightInfo(classId, isEnabled);
        }
    }
}