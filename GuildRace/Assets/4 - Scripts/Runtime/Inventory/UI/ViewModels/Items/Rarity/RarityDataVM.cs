using AD.Services.Localization;
using AD.Services.Router;
using UnityEngine;

namespace Game.Inventory
{
    public class RarityDataVM : ViewModel
    {
        public LocalizeKey NameKey { get; }
        public Color Color { get; }

        public RarityDataVM(RarityData data)
        {
            NameKey = data.NameKey;
            Color = data.Color;
        }

        protected override void InitSubscribes()
        {
        }
    }
}