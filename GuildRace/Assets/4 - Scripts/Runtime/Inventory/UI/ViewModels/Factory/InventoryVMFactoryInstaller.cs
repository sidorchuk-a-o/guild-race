using AD.Services.Router;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryVMFactoryInstaller : VMFactoryInstaller<InventoryVMFactory>
    {
        [SerializeField] private List<ItemsVMFactory> itemsFactories;

        protected override void PostInstall()
        {
            base.PostInstall();

            Instance.SetItemFactories(itemsFactories);
        }
    }
}