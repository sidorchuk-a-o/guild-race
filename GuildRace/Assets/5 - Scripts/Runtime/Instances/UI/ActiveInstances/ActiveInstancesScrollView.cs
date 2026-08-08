using AD.Router;
using UnityEngine;

namespace AD.Game.Instances
{
    public class ActiveInstancesScrollView : VMReactiveScrollView<ActiveInstanceVM>
    {
        [SerializeField] private RectTransform instanceItemPrefab;

        protected override VMItemHolder CreateItemHolder(ActiveInstanceVM viewModel)
        {
            return new VMItemHolder<ActiveInstanceVM>();
        }

        protected override RectTransform GetItemPrefab(ActiveInstanceVM viewModel)
        {
            return instanceItemPrefab;
        }
    }
}