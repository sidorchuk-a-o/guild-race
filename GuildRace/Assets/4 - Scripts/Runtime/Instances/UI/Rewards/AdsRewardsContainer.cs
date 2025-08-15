using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Instances
{
    public class AdsRewardsContainer : MonoBehaviour
    {
        [SerializeField] private AdsRewardItem itemPrefab;
        [Space]
        [SerializeField] private RectTransform itemContainer;
        [SerializeField] private RectTransform separator;

        private IObjectResolver resolver;
        private readonly List<AdsRewardItem> items = new();

        [Inject]
        public void Inject(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Init(AdsInstanceRewardsVM rewardsVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            var count = Mathf.Max(rewardsVM.Count, items.Count);

            separator.SetAsLastSibling();
            separator.SetActive(rewardsVM.Count > 0);

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
                    item.Init(itemVM, disp, ct);

                    var itemRect = item.transform as RectTransform;

                    itemRect.SetAsLastSibling();
                }
            }
        }
    }
}