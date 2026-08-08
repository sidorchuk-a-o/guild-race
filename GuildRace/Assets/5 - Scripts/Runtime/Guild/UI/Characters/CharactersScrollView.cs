using AD.Router;
using UnityEngine;

namespace AD.Game.Guild
{
    public class CharactersScrollView : VMReactiveScrollView<CharacterVM>
    {
        [Header("Prefab")]
        [SerializeField] private RectTransform characterItemPrefab;

        protected override VMItemHolder CreateItemHolder(CharacterVM viewModel)
        {
            return new VMItemHolder<CharacterVM>();
        }

        protected override RectTransform GetItemPrefab(CharacterVM viewModel)
        {
            return characterItemPrefab;
        }
    }
}