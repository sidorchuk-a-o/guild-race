using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Instances
{
    public class RewardsContainer : MonoBehaviour
    {
        [SerializeField] private RewardItem itemPrefab;
        [SerializeField] private RectTransform itemContainer;

        private IObjectResolver resolver;
        private readonly List<RewardItem> items = new();

        [Inject]
        public void Inject(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Init(InstanceRewardsVM rewardsVM, CancellationTokenSource ct)
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