using System.Collections.Generic;
using AD.ToolsCollection;
using Game.Instances;
using UnityEngine;
using VContainer;
using System.Linq;
using VContainer.Unity;

namespace Game.Craft
{
    public class ReagentSourceContainer : MonoBehaviour
    {
        [SerializeField] private ReagentSourceItem itemPrefab;

        private IObjectResolver resolver;
        private readonly List<ReagentSourceItem> items = new();

        private InstancesVMFactory instancesVMF;
        private InstanceRewardsVM sourcesVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF, IObjectResolver resolver)
        {
            this.instancesVMF = instancesVMF;
            this.resolver = resolver;
        }

        public void Init(ReagentDataVM dataVM, CompositeDisp disp)
        {
            sourcesVM = instancesVMF.GetRewardsByParam(dataVM.Id.ToString());
            sourcesVM.AddTo(disp);

            var count = Mathf.Max(sourcesVM.Count, items.Count);

            for (var i = 0; i < count; i++)
            {
                var item = items.ElementAtOrDefault(i);
                var itemVM = sourcesVM.ElementAtOrDefault(i);

                if (item == null)
                {
                    item = Instantiate(itemPrefab, transform);

                    resolver.InjectGameObject(item.gameObject);

                    items.Add(item);
                }

                item.SetActive(itemVM != null);

                if (itemVM != null)
                {
                    item.Init(itemVM, disp);
                }
            }
        }
    }
}