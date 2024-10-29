using Game.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class GuildBankParams
    {
        [SerializeField] private List<ItemsGridData> storagePages = new();

        public IReadOnlyList<ItemsGridData> StoragePages => storagePages;
    }
}