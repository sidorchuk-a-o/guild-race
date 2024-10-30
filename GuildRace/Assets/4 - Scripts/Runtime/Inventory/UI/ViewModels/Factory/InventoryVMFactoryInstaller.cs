using AD.Services.Router;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryVMFactoryInstaller : VMFactoryInstaller<InventoryVMFactory>
    {
        [SerializeField] private List<ItemsVMFactory> itemsFactories;
        [SerializeField] private List<ItemSlotsVMFactory> itemSlotsFactories;

        protected override void PostInstall()
        {
            base.PostInstall();

            Instance.SetItemsFactories(itemsFactories);
            Instance.SetItemSlotsFactories(itemSlotsFactories);
        }
    }
}