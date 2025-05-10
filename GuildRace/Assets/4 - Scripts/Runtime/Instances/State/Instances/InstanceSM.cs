using System.Linq;
using Newtonsoft.Json;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class InstanceSM
    {
        [ES3Serializable] private int id;
        [ES3Serializable] private UnitSM[] unitsSM;

        public InstanceSM(InstanceInfo info)
        {
            id = info.Id;
            unitsSM = info.BossUnits.Select(x => new UnitSM(x)).ToArray();
        }

        public InstanceInfo GetValue(InstancesConfig instanceConfig)
        {
            var data = instanceConfig.GetInstance(id);

            var bossUnits = data.BoosUnits
                .Select(x => GetUnit(x, instanceConfig))
                .Where(x => x != null);

            return new(data, bossUnits);
        }

        private UnitInfo GetUnit(UnitData data, InstancesConfig instanceConfig)
        {
            var unitSM = unitsSM.FirstOrDefault(x => x.Id == data.Id);

            return unitSM != null
                ? unitSM.GetValue(data)
                : new UnitInfo(data);
        }
    }
}