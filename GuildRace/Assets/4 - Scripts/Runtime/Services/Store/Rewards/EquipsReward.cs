using AD.Services.Localization;
using AD.Services.Store;
using Game.Instances;
using UnityEngine;

namespace Game.Store
{
    public class EquipsReward : RewardData
    {
        [Header("Equips")]
        [SerializeField] private int count;
        [SerializeField] private InstanceType instanceType;
        [Space]
        [SerializeField] private LocalizeKey previewKey;

        public int Count => count;
        public InstanceType InstanceType => instanceType;
        public LocalizeKey PreviewKey => previewKey;
    }
}