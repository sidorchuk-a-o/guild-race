using AD.Services.Router;
using UnityEngine;

namespace Game.Guild
{
    public class JoinRequestsScrollView : VMReactiveScrollView<JoinRequestVM>
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