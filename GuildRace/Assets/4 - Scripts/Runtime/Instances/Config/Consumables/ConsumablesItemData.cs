using System.Collections.Generic;
using AD.Services.Localization;
using Game.Inventory;
using UnityEngine;

namespace Game.Instances
{
    public class ConsumablesItemData : ItemData, IStackable
    {
        // View
        [SerializeField] private LocalizeKey descKey;
        // Params
        [SerializeField] private Rarity rarity;
        [SerializeField] private ItemStack stack;
        [SerializeField] private ConsumableType type;
        // Mechanic
        [SerializeField] private int mechanicId;
        [SerializeField] private List<string> mechanicParams;

        public LocalizeKey DescKey => descKey;
        public Rarity Rarity => rarity;
        public ItemStack Stack => stack;
        public ConsumableType Type => type;
        public int MechanicId => mechanicId;
        public IReadOnlyList<string> MechanicParams => mechanicParams;
    }
}