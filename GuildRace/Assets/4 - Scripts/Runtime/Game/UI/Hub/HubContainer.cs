using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game
{
    public class HubContainer : UIContainer
    {
        [Header("Guild & Player")]
        [SerializeField] private EmblemContainer emblemContainer;
        [SerializeField] private UIText guildNameText;
        [Space]
        [SerializeField] private UIText playerNicknameText;
        [SerializeField] private UIText playerGuildRankText;

        private GuildVM guildVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            guildVM = guildVMF.GetGuild();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            guildVM.AddTo(disp);

            await emblemContainer.Init(guildVM.EmblemVM);

            guildVM.GuildName
                .Subscribe(x => guildNameText.SetTextParams(x))
                .AddTo(disp);

            guildVM.PlayerNickname
                .Subscribe(x => playerNicknameText.SetTextParams(x))
                .AddTo(disp);

            guildVM.PlayerRank
                .Subscribe(x => playerGuildRankText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}