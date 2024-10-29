using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class RecruitingParams
    {
        [SerializeField] private int minRequestCount = 5;
        [SerializeField] private int maxRequestCount = 15;

        [SerializeField] private int requestLifetime = 60 * 60;
        [SerializeField] private int minNextRequestTime = 15;
        [SerializeField] private int maxNextRequestTime = 120;

        [SerializeField] private float weightSelectedRole = 60;
        [SerializeField] private float weightUnselectedRole = 30;

        [SerializeField] private int minEquipLevel = 10;
        [SerializeField] private List<int> characterGroupsWeights = new() { 15, 30, 60 };
        [SerializeField] private EquipGeneratorPhaseData firstPhase = new();
        [SerializeField] private EquipGeneratorPhaseData lastPhase = new();

        [SerializeField] private List<DefaultCharacterData> defaultCharacters;

        public int MinRequestCount => minRequestCount;
        public int MaxRequestCount => maxRequestCount;

        public int RequestLifetime => requestLifetime;
        public int MinNextRequestTime => minNextRequestTime;
        public int MaxNextRequestTime => maxNextRequestTime;

        public float WeightSelectedRole => weightSelectedRole;
        public float WeightUnselectedRole => weightUnselectedRole;

        public int MinEquipLevel => minEquipLevel;
        public IReadOnlyList<int> CharacterGroupsWeights => characterGroupsWeights;
        public EquipGeneratorPhaseData FirstPhase => firstPhase;
        public EquipGeneratorPhaseData LastPhase => lastPhase;

        public IReadOnlyList<DefaultCharacterData> DefaultCharacters => defaultCharacters;
    }
}