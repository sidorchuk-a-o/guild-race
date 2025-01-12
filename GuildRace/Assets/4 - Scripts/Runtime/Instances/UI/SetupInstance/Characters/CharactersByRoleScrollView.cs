using AD.Services.Router;
using Game.Guild;
using UnityEngine;

namespace Game.Instances
{
    public class CharactersByRoleScrollView : VMScrollView<CharacterVM>
    {
        [Header("Prefab")]
        [SerializeField] private RectTransform characterItemPrefab;

        private ActiveInstanceVM activeInstanceVM;

        public void SetInstance(ActiveInstanceVM activeInstanceVM)
        {
            this.activeInstanceVM = activeInstanceVM;
        }

        protected override VMItemHolder CreateItemHolder(CharacterVM viewModel)
        {
            return new VMItemHolder<CharacterVM>();
        }

        protected override RectTransform GetItemPrefab(CharacterVM viewModel)
        {
            return characterItemPrefab;
        }

        protected override void UpdateViewsHolder(VMItemHolder newOrRecycled)
        {
            base.UpdateViewsHolder(newOrRecycled);

            var viewModel = this[index: newOrRecycled.ItemIndex];
            var characterInstanceItem = newOrRecycled.root.GetComponent<CharacterInstanceItem>();

            characterInstanceItem.Init(viewModel, activeInstanceVM);
        }
    }
}