using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class UIParams
    {
        // Drag & Drop
        [SerializeField] private List<PickupHandler> pickupHandlers;
        [SerializeField] private List<ReleaseHandler> placeHandlers;
        [SerializeField] private List<ReleaseHandler> splitHandlers;
        [SerializeField] private List<ReleaseHandler> rollbackHandlers;
        // Options
        [SerializeField] private List<OptionHandler> optionHandlers;

        private Dictionary<OptionKey, OptionHandler> optionsCache;
        private Dictionary<OptionKey, int> optionOrdersCache;

        public IReadOnlyList<PickupHandler> PickupHandlers => pickupHandlers;
        public IReadOnlyList<ReleaseHandler> PlaceHandlers => placeHandlers;
        public IReadOnlyList<ReleaseHandler> SplitHandlers => splitHandlers;
        public IReadOnlyList<ReleaseHandler> RollbackHandlers => rollbackHandlers;
        public IReadOnlyList<OptionHandler> OptionHandlers => optionHandlers;

        public OptionHandler GetOption(OptionKey option)
        {
            optionsCache ??= optionHandlers.ToDictionary(x => x.Key, x => x);

            return optionsCache[option];
        }

        public int GetOptionOrder(OptionKey option)
        {
            optionOrdersCache ??= optionHandlers.ToDictionary(x => x.Key, optionHandlers.IndexOf);

            return optionOrdersCache[option];
        }
    }
}