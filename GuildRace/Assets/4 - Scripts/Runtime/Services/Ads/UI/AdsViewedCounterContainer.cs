using System.Collections.Generic;
using System.Linq;
using AD.Services.Ads;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;

namespace Game.Ads
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

                var hasItem = item == null;
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