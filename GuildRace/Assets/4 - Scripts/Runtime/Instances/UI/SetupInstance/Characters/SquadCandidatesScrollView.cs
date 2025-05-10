using AD.Services.Router;
using UnityEngine;

namespace Game.Instances
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