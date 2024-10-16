using AD.Services.Router;
using Game.Guild;
using UnityEngine;

namespace Game
{
    public class CharactersScrollView : VMScrollView<CharacterVM>
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