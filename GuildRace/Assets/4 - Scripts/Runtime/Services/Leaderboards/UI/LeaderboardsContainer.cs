using AD.UI;
using AD.Services.Router;
using AD.Services.Leaderboards;
using AD.ToolsCollection;
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Game.Leaderboards
{
    public class LeaderboardsContainer : UIContainer
    {
        [SerializeField] private List<UIText> headers;
        [SerializeField] private List<PlayersScrollView> playersScrolls;

        private LeaderboardsVM leaderboardsVM;

        [Inject]
        public void Inject(LeaderboardsVMFactory leaderboardsVMF)
        {
            leaderboardsVM = leaderboardsVMF.GetLeaderboards(update: true);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            leaderboardsVM.AddTo(disp);

            for (var i = 0; i < leaderboardsVM.Count; i++)
            {
                var leaderboardVM = leaderboardsVM[i];

                var header = headers[i];
                var playerScroll = playersScrolls[i];

                header.SetTextParams(leaderboardVM.TitleKey);
                playerScroll.Init(leaderboardVM.PlayersVM, true);
            }
        }
    }
}