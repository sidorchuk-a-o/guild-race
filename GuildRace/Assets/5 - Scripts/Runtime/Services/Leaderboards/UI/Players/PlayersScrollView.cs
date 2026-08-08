using AD.Leaderboards;
using AD.Router;
using UnityEngine;

namespace AD.Game.Leaderboards
{
    public class PlayersScrollView : VMReactiveScrollView<PlayerVM>
    {
        [SerializeField] private RectTransform playerItemPrefab;

        protected override VMItemHolder CreateItemHolder(PlayerVM viewModel)
        {
            return new VMItemHolder<PlayerVM>();
        }

        protected override RectTransform GetItemPrefab(PlayerVM viewModel)
        {
            return playerItemPrefab;
        }
    }
}