using AD.Services.Router;
using AD.Services.Store;
using UnityEngine;

namespace Game.Store
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