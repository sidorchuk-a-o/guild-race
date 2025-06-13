using AD.ToolsCollection;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    [CreateAssetMenu(fileName = "ConsumableMechanicsUIParams", menuName = "Instances/UI/Consumable Mechanic Params", order = 100)]
    public class ConsumableMechanicsUIParams : ScriptableData
    {
        [SerializeField] private List<ConsumableMechanicUIParams> mechanicParams;

        private Dictionary<int, ConsumableMechanicUIParams> paramsCache;

        public ConsumableMechanicUIParams GetParams(int mechanicId)
        {
            paramsCache ??= mechanicParams.ToDictionary(x => x.MechanicId, x => x);
            paramsCache.TryGetValue(mechanicId, out var data);

            return data;
        }
    }
}