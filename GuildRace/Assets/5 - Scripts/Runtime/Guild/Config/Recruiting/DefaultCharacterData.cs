using System;
using UnityEngine;

namespace AD.Game.Guild
{
    [Serializable]
    public class DefaultCharacterData
    {
        [SerializeField] private ClassId classId;
        [SerializeField] private SpecializationId specId;

        public ClassId ClassId => classId;
        public SpecializationId SpecId => specId;
    }
}