using AD.Game.Instances;
using AD.Localization;
using AD.Store;
using UnityEngine;

namespace AD.Game.Store
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