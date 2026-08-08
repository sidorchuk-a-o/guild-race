using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AD.Game.Guild
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