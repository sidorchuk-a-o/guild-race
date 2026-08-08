using AD.Router;
using UnityEngine;

namespace AD.Game.Instances
{
    public class SquadCandidatesScrollView : VMScrollView<SquadCandidateVM>
    {
        [Header("Prefab")]
        [SerializeField] private RectTransform characterItemPrefab;

        protected override VMItemHolder CreateItemHolder(SquadCandidateVM viewModel)
        {
            return new VMItemHolder<SquadCandidateVM>();
        }

        protected override RectTransform GetItemPrefab(SquadCandidateVM viewModel)
        {
            return characterItemPrefab;
        }
    }
}