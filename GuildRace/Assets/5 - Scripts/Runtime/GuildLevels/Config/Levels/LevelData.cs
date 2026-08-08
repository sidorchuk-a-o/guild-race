using AD.Localization;
using AD.Store;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.GuildLevels
{
    public class LevelData : ScriptableEntity<string>
    {
        [SerializeField] private LocalizeKey descKey;
        [Space]
        [SerializeField] private LevelMechanic mechanic;
        [SerializeField] private CurrencyAmountData unlockPrice;

        public LocalizeKey DescKey => descKey;
        public LevelMechanic Mechanic => mechanic;
        public CurrencyAmount UnlockPrice => unlockPrice;
    }
}