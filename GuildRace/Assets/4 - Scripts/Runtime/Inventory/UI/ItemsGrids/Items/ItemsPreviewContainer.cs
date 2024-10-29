using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class ItemsPreviewContainer : MonoBehaviour
    {
        private readonly List<ItemInGridComponent> items = new();

        private InventoryVMFactory inventoryVMF;
        private ItemsVM itemsVM;

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF)
        {
            this.inventoryVMF = inventoryVMF;
        }

        public async void Init(ItemsGridVM gridVM, CompositeDisp disp)
        {
            itemsVM = gridVM.Items;

            itemsVM.ObserveAdd()
                .Subscribe(args => AddItemCallback(args.Index, disp))
                .AddTo(disp);

            itemsVM.ObserveRemove()
                .Subscribe(args => RemoveItemCallback(args.Index, disp))
                .AddTo(disp);

            // clear items
            inventoryVMF.ReturnItems(items);

            items.Clear();

            // update items
            for (var i = 0; i < itemsVM.Count; i++)
            {
                var itemVM = itemsVM.ElementAtOrDefault(i);

                await UpdateItem(item: null, itemVM, disp);
            }
        }

        private async void AddItemCallback(int index, CompositeDisp disp)
        {
            var itemVM = itemsVM.ElementAtOrDefault(index);

            await UpdateItem(item: null, itemVM, disp);
        }

        private async void RemoveItemCallback(int index, CompositeDisp disp)
        {
            var item = items.ElementAtOrDefault(index);

            await UpdateItem(item, itemVM: null, disp);
        }

        private async UniTask UpdateItem(ItemInGridComponent item, ItemVM itemVM, CompositeDisp disp)
        {
            var hasItem = item != null;
            var hasVM = itemVM != null;

            if (hasVM)
            {
                if (!hasItem)
                {
                    var itemGO = await inventoryVMF.RentItemInGridAsync(itemVM);

                    item = itemGO.GetComponent<ItemInGridComponent>();
                    item.SetParent(transform);

                    items.Add(item);
                }

                var itemRect = item.transform as RectTransform;
                var itemBounds = itemVM.BoundsVM.Value;

                itemRect.ApplyItemBounds(itemBounds);

                item.Init(itemVM, disp);
            }
            else if (hasItem)
            {
                items.Remove(item);
                inventoryVMF.ReturnItem(item);
            }
        }

        // == OnDestroy ==

        private void OnDestroy()
        {
            inventoryVMF.ReturnItems(items);
            items.Clear();
        }
    }
}