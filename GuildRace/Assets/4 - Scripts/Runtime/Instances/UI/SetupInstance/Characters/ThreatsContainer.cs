#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Instances
{
    public class ThreatsContainer : MonoBehaviour
    {
        [SerializeField] private ThreatItem itemPrefab;

        private readonly List<ThreatItem> threatItems = new();

        public async UniTask Init(ThreatsVM threatsVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            var initTasks = ListPool<UniTask>.Get();
            var count = Mathf.Max(threatsVM.Count, threatItems.Count);

            for (var i = 0; i < count; i++)
            {
                var item = threatItems.ElementAtOrDefault(i);
                var itemVM = threatsVM.ElementAtOrDefault(i);

                if (item == null)
                {
                    item = Instantiate(itemPrefab, transform);

                    threatItems.Add(item);
                }

                if (itemVM == null)
                {
                    item.SetActive(false);
                }
                else
                {
                    item.SetActive(true);

                    initTasks.Add(item.Init(itemVM, disp, ct));
                }
            }

            await UniTask.WhenAll(initTasks);

            initTasks.ReleaseListPool();
        }
    }
}