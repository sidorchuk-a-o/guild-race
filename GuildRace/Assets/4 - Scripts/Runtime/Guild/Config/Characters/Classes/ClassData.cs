using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Guild
{
    public class ClassData : ScriptableEntity
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private List<SpecializationData> specs;

        private Dictionary<SpecializationId, SpecializationData> specsCache;

        public LocalizeKey NameKey => nameKey;
        public IReadOnlyList<SpecializationData> Specs => specs;

        public SpecializationData GetSpecialization(SpecializationId specId)
        {
            specsCache ??= specs.ToDictionary(x => (SpecializationId)x.Id, x => x);

            return specsCache[specId];
        }
    }
}