using AD.Services.Router;
using UnityEngine;

namespace Game.Instances
{
    public class UnitsScrollView : VMScrollView<UnitVM>
    {
        [SerializeField] private RectTransform unitItemPrefab;

        protected override VMItemHolder CreateItemHolder(UnitVM viewModel)
        {
            return new VMItemHolder<UnitVM>();
        }

        protected override RectTransform GetItemPrefab(UnitVM viewModel)
        {
            return unitItemPrefab;
        }
    }
}