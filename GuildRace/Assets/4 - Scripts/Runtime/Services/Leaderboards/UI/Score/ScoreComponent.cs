using System.Threading;
using AD.Services.Leaderboards;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.UI;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Leaderboards
{
    public class ScoreComponent : MonoBehaviour
    {
        [SerializeField] private LeaderboardKey leaderboardKey;
        [Space]
        [SerializeField] private UIText scoreText;
        [SerializeField] private ScoreChangedContainer changedContainer;
        [Space]
        [SerializeField] private TooltipComponent tooltipComponent;

        private LeaderboardVM leaderboardVM;

        [Inject]
        public void Inject(LeaderboardsVMFactory leaderboardsVMF)
        {
            leaderboardVM = leaderboardsVMF.GetLeaderboard(leaderboardKey);
        }

        public void Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            leaderboardVM.AddTo(disp);

            changedContainer.Init(leaderboardVM, disp);
            tooltipComponent.Init(leaderboardVM);

            leaderboardVM.ScoreStr
                .Subscribe(x => scoreText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}