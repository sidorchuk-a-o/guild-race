using AD.ToolsCollection;
using System;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class DefaultCharacterData : Entity<string>
    {
        [SerializeField] private ClassId classId;
        [SerializeField] private SpecializationId specId;

        public ClassId ClassId => classId;
        public SpecializationId SpecId => specId;
    }
}