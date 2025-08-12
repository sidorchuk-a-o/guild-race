using AD.UI;
using AD.Services.Leaderboards;
using AD.ToolsCollection;
using System.Threading;
using UnityEngine;
using VContainer;
using UniRx;

namespace Game.Leaderboards
{
    public class LeaderboardComponent : MonoBehaviour
    {
        [SerializeField] private LeaderboardKey leaderboardKey;
        [Space]
        [SerializeField] private UIText scoreText;
        [SerializeField] private ScoreChangedContainer changedContainer;

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

            leaderboardVM.ScoreStr
                .Subscribe(x => scoreText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}