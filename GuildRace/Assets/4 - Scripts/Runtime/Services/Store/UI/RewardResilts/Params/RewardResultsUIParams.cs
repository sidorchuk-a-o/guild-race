using System;
using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Store
{
    [CreateAssetMenu(fileName = "RewardResultsUIParams", menuName = "Store/UI/Reward Result Params", order = 100)]
    public class RewardResultsUIParams : ScriptableData
    {
        [SerializeField] private List<RewardResultUIParams> resultParams;

        private Dictionary<Type, RewardResultUIParams> paramsCache;

        public RewardResultUIParams GetParams(Type rewardType)
        {
            paramsCache ??= resultParams.ToDictionary(x => x.RewardType, x => x);
            paramsCache.TryGetValue(rewardType, out var data);

            return data;
        }
    }
}