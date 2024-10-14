using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class RecruitingModuleData
    {
        [SerializeField] private int minRequestCount = 5;
        [SerializeField] private int maxRequestCount = 15;

        [SerializeField] private int requestLifetime = 60 * 60;
        [SerializeField] private int minNextRequestTime = 15;
        [SerializeField] private int maxNextRequestTime = 120;

        [SerializeField] private float weightSelectedRole = 60;
        [SerializeField] private float weightUnselectedRole = 30;

        [SerializeField] private List<DefaultCharacterData> defaultCharacters;

        public int MinRequestCount => minRequestCount;
        public int MaxRequestCount => maxRequestCount;

        public int RequestLifetime => requestLifetime * 1000;
        public int MinNextRequestTime => minNextRequestTime * 1000;
        public int MaxNextRequestTime => maxNextRequestTime * 1000;

        public float WeightSelectedRole => weightSelectedRole;
        public float WeightUnselectedRole => weightUnselectedRole;

        public IReadOnlyList<DefaultCharacterData> DefaultCharacters => defaultCharacters;
    }
}