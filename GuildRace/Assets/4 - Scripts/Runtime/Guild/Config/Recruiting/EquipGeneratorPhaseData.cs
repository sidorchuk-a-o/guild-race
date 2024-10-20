using System;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class EquipGeneratorPhaseData
    {
        [SerializeField] private int minEquipCount;
        [SerializeField] private int maxEquipCount;
        [SerializeField] private int minEquipLevel;
        [SerializeField] private int maxEquipLevel;

        public int MinEquipCount => minEquipCount;
        public int MaxEquipCount => maxEquipCount;
        public int MinEquipLevel => minEquipLevel;
        public int MaxEquipLevel => maxEquipLevel;
    }
}