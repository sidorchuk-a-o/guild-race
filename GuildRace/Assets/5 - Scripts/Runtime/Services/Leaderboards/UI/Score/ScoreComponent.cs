using AD.Game.UI;
using AD.Leaderboards;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;
using VContainer;

namespace AD.Game.Leaderboards
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
            leaderboardVM = leaderboardsVMF.GetLeaderboard(leaderboardKey, quantityTop: 10, quantityAround: 10);
        }

        public void Init(CompositeDisp disp)
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