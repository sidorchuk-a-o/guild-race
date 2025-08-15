using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Store
{
    public class RewardResultsContainer : MonoBehaviour
    {
        [SerializeField] private RewardResultItem itemPrefab;
        [SerializeField] private RectTransform itemContainer;

        private IObjectResolver resolver;
        private readonly List<RewardResultItem> items = new();

        [Inject]
        public void Inject(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Init(RewardResultsVM rewardsVM, CancellationTokenSource ct)
        {
            var count = Mathf.Max(rewardsVM.Count, items.Count);

            for (var i = 0; i < count; i++)
            {
                var item = items.ElementAtOrDefault(i);
                var itemVM = rewardsVM.ElementAtOrDefault(i);

                if (item == null)
                {
                    item = Instantiate(itemPrefab, itemContainer);

                    resolver.InjectGameObject(item.gameObject);

                    items.Add(item);
                }

                item.SetActive(itemVM != null);

                if (itemVM != null)
                {
                    item.Init(itemVM, ct);
                }
            }
        }
    }
}