using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Game.Store
{
    public class RewardPreviewsContainer : MonoBehaviour
    {
        [SerializeField] private RewardPreviewItem itemPrefab;

        private readonly List<RewardPreviewItem> items = new();
        private IObjectResolver resolver;

        [Inject]
        public void Inject(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }

        public async UniTask Init(RewardVM rewardVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            var rewardPreviewsVM = rewardVM.RewardPreviewsVM;
            var count = Mathf.Max(rewardPreviewsVM.Count, items.Count);
            var tasks = ListPool<UniTask>.Get();

            for (var i = 0; i < count; i++)
            {
                var item = items.ElementAtOrDefault(i);
                var itemVM = rewardPreviewsVM.ElementAtOrDefault(i);

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
                    tasks.Add(item.Init(itemVM, ct));
                }
            }

            await UniTask.WhenAll(tasks);
        }
    }
}