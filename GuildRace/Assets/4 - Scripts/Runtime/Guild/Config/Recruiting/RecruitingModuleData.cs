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
        [SerializeField] private float requestLifetime = 3600;

        [SerializeField] private float minNextRequestTime = 15;
        [SerializeField] private float maxNextRequestTime = 120;

        [SerializeField] private float weightSelectedRole = 60;
        [SerializeField] private float weightUnselectedRole = 30;

        [SerializeField] private List<DefaultCharacterData> defaultCharacters;
    }
}