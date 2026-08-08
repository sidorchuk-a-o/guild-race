using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class RarityData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private Color color = Color.white;

        public LocalizeKey NameKey => nameKey;
        public Color Color => color;
    }
}