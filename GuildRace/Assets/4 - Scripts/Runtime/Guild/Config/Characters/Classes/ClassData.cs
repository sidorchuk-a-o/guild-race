using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Guild
{
    public class ClassData : ScriptableEntity
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private List<SpecializationData> specs;

        public LocalizeKey NameKey => nameKey;
        public IReadOnlyList<SpecializationData> Specs => specs;
    }
}