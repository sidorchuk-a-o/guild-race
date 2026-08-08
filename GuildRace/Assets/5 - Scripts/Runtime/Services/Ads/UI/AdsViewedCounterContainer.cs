using System.Collections.Generic;
using System.Linq;
using AD.Ads;
using AD.ToolsCollection;
using R3;
using UnityEngine;

namespace AD.Game.Ads
{
    public class AdsViewedCounterContainer : MonoBehaviour
    {
        [SerializeField] private AdsViewedIndicator itemPrefab;

        public readonly List<AdsViewedIndicator> items = new();

        public void Init(AdsViewedCounterVM counterVM, CompositeDisp disp)
        {
            var count = Mathf.Max(counterVM.MaxCount, items.Count);

            for (var i = 0; i < count; i++)
            {
                var item = items.ElementAtOrDefault(i);

                var hasItem = item is null;
                var hasCounter = i < counterVM.MaxCount;

                if (hasItem)
                {
                    item = Instantiate(itemPrefab, transform);

                    items.Add(item);
                }

                item.SetActive(hasCounter);
            }

            counterVM.Count
                .Subscribe(CountChangedCallback)
                .AddTo(disp);
        }

        private void CountChangedCallback(int count)
        {
            for (var i = 0; i < items.Count; i++)
            {
                items[i].SetState(i < count);
            }
        }
    }
}