using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class ClassRoleSelectorsSM
    {
        [ES3Serializable] private List<ClassRoleSelectorSM> values;

        public ClassRoleSelectorsSM(IEnumerable<ClassRoleSelectorInfo> values)
        {
            this.values = values
                .Select(x => new ClassRoleSelectorSM(x))
                .ToList();
        }

        public IEnumerable<ClassRoleSelectorInfo> GetValues()
        {
            return values.Select(x => x.GetValue());
        }
    }
}