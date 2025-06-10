using UnityEngine;
using System;
using UnityEngine.AddressableAssets;

namespace Game.Instances
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