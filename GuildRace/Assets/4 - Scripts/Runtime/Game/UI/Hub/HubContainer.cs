using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Store;
using Game.Weekly;
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
        [SerializeField] private UIText playerNameText;
        [SerializeField] private UIText playerGuildRankText;
        [Space]
        [SerializeField] private WeeklyItem weeklyItem;
        [SerializeField] private CurrenciesContainer currenciesContainer;

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

            weeklyItem.Init(disp);
            currenciesContainer.Init(disp, ct);

            await emblemContainer.Init(guildVM.EmblemVM);

            guildVM.GuildName
                .Subscribe(x => guildNameText.SetTextParams(x))
                .AddTo(disp);

            guildVM.PlayerName
                .Subscribe(x => playerNameText.SetTextParams(x))
                .AddTo(disp);

            guildVM.PlayerRank
                .Subscribe(x => playerGuildRankText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}