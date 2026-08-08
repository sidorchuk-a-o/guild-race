using AD.Router;
using AD.Store;
using UnityEngine;

namespace AD.Game.Store
{
    public class StoreShelvesScrollView : VMScrollView<StoreShelfVM>
    {
        [SerializeField] private RectTransform shelfPrefab;

        protected override VMItemHolder CreateItemHolder(StoreShelfVM viewModel)
        {
            return new VMItemHolder<StoreShelfVM>();
        }

        protected override RectTransform GetItemPrefab(StoreShelfVM viewModel)
        {
            return shelfPrefab;
        }
    }
}