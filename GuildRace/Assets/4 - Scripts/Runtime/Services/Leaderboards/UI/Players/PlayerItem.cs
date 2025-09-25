using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Router;
using AD.Services.Leaderboards;
using AD.ToolsCollection;
using UnityEngine;
using Game.Guild;
using VContainer;

namespace Game.Leaderboards
{
    public class PlayerItem : VMScrollItem<PlayerVM>
    {
        [Header("Player")]
        [SerializeField] private UIText rankText;
        [SerializeField] private UIText scoreText;
        [SerializeField] private UIText nameText;
        [SerializeField] private EmblemContainer emblemPreview;
        [Space]
        [SerializeField] private UIStates playerState;
        [SerializeField] private string currentKey = "current";
        [SerializeField] private UIStates topState;
        [SerializeField] private string[] topKeys = new[] { "top1", "top2", "top3" };

        private GuildVMFactory guildVMF;
        private GuildScoreVM extraVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            this.guildVMF = guildVMF;
        }

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            extraVM = guildVMF.GetLeaderboardExtra(ViewModel.ExtraData);
            extraVM.AddTo(disp);

            rankText.SetTextParams(ViewModel.Rank);
            scoreText.SetTextParams(ViewModel.Score);
            nameText.SetTextParams(extraVM.GuildName);

            playerState.SetState(ViewModel.IsCurrent ? currentKey : UISelectable.defaultStateKey);
            topState.SetState(ViewModel.Rank <= 3 ? topKeys[ViewModel.Rank - 1] : UISelectable.defaultStateKey);

            await emblemPreview.Init(extraVM.EmblemVM);
        }
    }
}