using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Craft
{
    public class CraftConfig : ScriptableConfig
    {
        [SerializeField] private List<VendorData> vendors;
        [SerializeField] private ReagentsParams reagentsParams = new();

        public IReadOnlyList<VendorData> Vendors => vendors;
        public ReagentsParams ReagentsParams => reagentsParams;
    }
}