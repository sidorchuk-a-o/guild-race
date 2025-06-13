using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    [Serializable]
    public class RewardMechanicUIParams
    {
        [SerializeField] private int mechanicId;
        [SerializeField] private AssetReference itemRef;

        public int MechanicId => mechanicId;
        public AssetReference ItemRef => itemRef;
    }
}