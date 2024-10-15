using AD.Services.Router;
using Game.Guild;
using UnityEngine;

namespace Game
{
    public class JoinRequestsScrollView : VMScrollView<JoinRequestVM>
    {
        [Header("Prefab")]
        [SerializeField] private RectTransform requestItemPrefab;

        protected override VMItemHolder CreateItemHolder(JoinRequestVM viewModel)
        {
            return new VMItemHolder<JoinRequestVM>();
        }

        protected override RectTransform GetItemPrefab(JoinRequestVM viewModel)
        {
            return requestItemPrefab;
        }
    }
}