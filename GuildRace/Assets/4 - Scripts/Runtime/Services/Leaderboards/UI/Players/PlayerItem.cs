#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Leaderboards;
using AD.Services.Router;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Leaderboards
{
    public class PlayerItem : VMScrollItem<PlayerVM>
    {
        [Header("Player")]
        [SerializeField] private UIText rankText;
        [SerializeField] private UIText scoreText;
        [SerializeField] private UIText extraDataText;
        [Space]
        [SerializeField] private UIStates playerState;
        [SerializeField] private string currentKey = "current";
        [SerializeField] private UIStates topState;
        [SerializeField] private string[] topKeys = new[] { "top1", "top2", "top3" };

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            rankText.SetTextParams(ViewModel.Rank);
            scoreText.SetTextParams(ViewModel.Score);
            extraDataText.SetTextParams(ViewModel.ExtraData);

            playerState.SetState(ViewModel.IsCurrent ? currentKey : UISelectable.defaultStateKey);
            topState.SetState(ViewModel.Rank <= 3 ? topKeys[ViewModel.Rank - 1] : UISelectable.defaultStateKey);
        }
    }
}