using AD.Services.Router;
using UnityEngine;

namespace Game.Instances
{
    public class ActiveInstancesScrollView : VMScrollView<ActiveInstanceVM>
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