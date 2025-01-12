using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class InstanceData : ScriptableEntity<int>
    {
        // params
        [SerializeField] private InstanceType type;
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        // map logic
        [SerializeField] private AssetReference mapRef;
        [SerializeField] private AssetReference uiRef;
        // boos units
        [SerializeField] private List<UnitData> boosUnits;
        [SerializeField] private List<UnitData> trashUnits;

        public InstanceType Type => type;
        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;

        public AssetReference MapRef => mapRef;
        public AssetReference UIRef => uiRef;

        public IReadOnlyList<UnitData> BoosUnits => boosUnits;
        public IReadOnlyList<UnitData> TrashUnits => trashUnits;
    }
}