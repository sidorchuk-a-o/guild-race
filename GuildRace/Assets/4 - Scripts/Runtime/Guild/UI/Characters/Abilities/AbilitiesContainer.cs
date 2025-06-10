using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Guild
{
    public class AbilitiesContainer : MonoBehaviour
    {
        [SerializeField] private AbilityItem itemPrefab;

        private IObjectResolver resolver;
        private readonly List<AbilityItem> items = new();

        [Inject]
        public void Inject(IObjectResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Init(AbilitiesVM abilitiesVM, CancellationTokenSource ct)
        {
            var count = Mathf.Max(abilitiesVM.Count, items.Count);

            for (var i = 0; i < count; i++)
            {
                var item = items.ElementAtOrDefault(i);
                var itemVM = abilitiesVM.ElementAtOrDefault(i);

                if (item == null)
                {
                    item = Instantiate(itemPrefab, transform);

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