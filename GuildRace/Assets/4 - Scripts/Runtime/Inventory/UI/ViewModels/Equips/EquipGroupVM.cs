using System.Threading;
using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Inventory
{
    public class EquipGroupVM : ViewModel
    {
        private readonly EquipGroupData data;
        private readonly InventoryVMFactory inventoryVMF;

        public EquipGroup Group { get; }
        public LocalizeKey NameKey { get; }

        public EquipGroupVM(EquipGroupData data, InventoryVMFactory inventoryVMF)
        {
            this.data = data;
            this.inventoryVMF = inventoryVMF;

            Group = data.Id;
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }

        public UniTask<Sprite> LoadIcon(CancellationTokenSource ct)
        {
            return inventoryVMF.RentImage(data.IconRef, ct.Token);
        }
    }
}