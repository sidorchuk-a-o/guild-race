using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class CompleteChanceData
    {
        [SerializeField] private InstanceType instanceType;
        [Space]
        [SerializeField] private float charactersCountMod;
        [SerializeField] private float resolveThreatMod;
        [SerializeField] private int maxResolveCount;
        [SerializeField] private List<ConsumableChanceData> consumables;
        [SerializeField] private UnitParamsChanceData healthMod;
        [SerializeField] private UnitParamsChanceData powerMod;

        public InstanceType InstanceType => instanceType;

        public float CharactersCountMod => charactersCountMod;

        public float ResolveThreatMod => resolveThreatMod;
        public int MaxResolveCount => maxResolveCount;

        public UnitParamsChanceData HealthMod => healthMod;
        public UnitParamsChanceData PowerMod => powerMod;

        public ConsumableChanceData GetConsumableChance(Rarity rarity)
        {
            return consumables.FirstOrDefault(x => x.Rarity == rarity);
        }
    }
}