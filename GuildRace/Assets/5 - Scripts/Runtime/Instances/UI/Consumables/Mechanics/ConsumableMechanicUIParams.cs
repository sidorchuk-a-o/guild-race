using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Instances
{
    [Serializable]
    public class ConsumableMechanicUIParams
    {
        [SerializeField] private int mechanicId;
        [SerializeField] private AssetReference containerRef;

        public int MechanicId => mechanicId;
        public AssetReference ContainerRef => containerRef;
    }
}