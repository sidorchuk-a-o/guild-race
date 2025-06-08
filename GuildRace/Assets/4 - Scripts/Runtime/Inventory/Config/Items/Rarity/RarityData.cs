using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    public class RarityData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private Color color = Color.white;

        public LocalizeKey NameKey => nameKey;
        public Color Color => color;
    }
}