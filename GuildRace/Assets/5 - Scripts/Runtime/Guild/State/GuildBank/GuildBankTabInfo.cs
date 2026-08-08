using AD.Game.Inventory;
using AD.Localization;
using UnityEngine.AddressableAssets;

namespace AD.Game.Guild
{
    public class GuildBankTabInfo
    {
        public string Id { get; }

        public LocalizeKey NameKey { get; }
        public AssetReference IconRef { get; }

        public ItemsGridInfo Grid { get; }

        public GuildBankTabInfo(GuildBankTabData data, ItemsGridInfo grid)
        {
            Id = data.Id;
            NameKey = data.NameKey;
            IconRef = data.IconRef;
            Grid = grid;
        }
    }
}