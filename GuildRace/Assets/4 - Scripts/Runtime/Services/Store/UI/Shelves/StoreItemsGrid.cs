using System.Linq;
using System.Threading;
using System.Collections.Generic;
using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Store
{
    public class StoreItemsGrid : MonoBehaviour
    {
        [SerializeField] private StoreItemItem itemPrefab;

        private readonly List<StoreItemItem> items = new();
        private IObjectResolver resolver;

        [Inject]
        public void Inject(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Init(StoreItemsVM storeItemsVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            var count = Mathf.Max(storeItemsVM.Count, items.Count);

            for (var i = 0; i < count; i++)
            {
                var item = items.ElementAtOrDefault(i);
                var itemVM = storeItemsVM.ElementAtOrDefault(i);

                var hasItem = item != null;
                var hasItemVM = itemVM != null;

                if (!hasItem)
                {
                    item = Instantiate(itemPrefab, transform);

                    resolver.InjectGameObject(item.gameObject);

                    items.Add(item);
                }

                item.SetActive(hasItemVM);

                if (hasItemVM)
                {
                    item.Init(itemVM, disp, ct);
                }
            }
        }
    }
}