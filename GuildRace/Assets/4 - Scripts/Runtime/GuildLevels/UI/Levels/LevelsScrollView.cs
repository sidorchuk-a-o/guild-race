using AD.Services.Router;
using UnityEngine;

namespace Game.GuildLevels
{
    public class LevelsScrollView : VMScrollView<LevelVM>
    {
        [SerializeField] private RectTransform levelItemPrefab;

        protected override VMItemHolder CreateItemHolder(LevelVM viewModel)
        {
            return new VMItemHolder<LevelVM>();
        }

        protected override RectTransform GetItemPrefab(LevelVM viewModel)
        {
            return levelItemPrefab;
        }
    }
}