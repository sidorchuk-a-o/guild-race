using AD.UI;
using AD.Services.Store;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.Guild;
using Game.Store;
using Game.Weekly;
using Game.Leaderboards;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using UniRx;

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
        [SerializeField] private ScoreComponent leaderboardComponent;
        [SerializeField] private CurrenciesContainer currenciesContainer;

        private IStoreService storeService;
        private GuildVM guildVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF, IStoreService storeService)
        {
            this.storeService = storeService;

            guildVM = guildVMF.GetGuild();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            guildVM.AddTo(disp);

            weeklyItem.Init(disp);
            currenciesContainer.Init(disp, ct);
            leaderboardComponent.Init(disp);

            await emblemContainer.Init(guildVM.EmblemVM);

            playerGuildRankText.SetTextParams(guildVM.PlayerRank);

            guildVM.GuildName
                .Subscribe(x => guildNameText.SetTextParams(x))
                .AddTo(disp);

            guildVM.PlayerName
                .Subscribe(x => playerNameText.SetTextParams(x))
                .AddTo(disp);

            storeService.OnPurchaseResult
                .Subscribe(PurchaseResultCallback)
                .AddTo(disp);
        }

        private void PurchaseResultCallback()
        {
            Router.Push(RouteKeys.Hub.StoreRewards);
        }
    }
}