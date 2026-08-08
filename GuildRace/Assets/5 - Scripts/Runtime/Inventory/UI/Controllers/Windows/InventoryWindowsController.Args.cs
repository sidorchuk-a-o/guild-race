using UnityEngine.AddressableAssets;

namespace AD.Game.Inventory
{
    public class OpenItemWindowArgs
    {
        public string ItemId { get; set; }
        public AssetReference WindowRef { get; set; }
    }
}