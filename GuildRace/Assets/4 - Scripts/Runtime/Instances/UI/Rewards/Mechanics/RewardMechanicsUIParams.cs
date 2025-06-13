using AD.ToolsCollection;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game.Instances
{
    [CreateAssetMenu(fileName = "RewardMechanicsUIParams", menuName = "Instances/UI/Reward Mechanic Params", order = 100)]
    public class RewardMechanicsUIParams : ScriptableData
    {
        [SerializeField] private List<RewardMechanicUIParams> mechanicParams;

        private Dictionary<int, RewardMechanicUIParams> paramsCache;

        public RewardMechanicUIParams GetParams(int mechanicId)
        {
            paramsCache ??= mechanicParams.ToDictionary(x => x.MechanicId, x => x);
            paramsCache.TryGetValue(mechanicId, out var data);

            return data;
        }
    }
}