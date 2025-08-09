using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Store
{
    [Serializable]
    public class RewardResultUIParams
    {
        [SerializeField] private string rewardType;
        [SerializeField] private AssetReference itemRef;

        public Type RewardType => Type.GetType(rewardType);
        public AssetReference ItemRef => itemRef;
    }
}