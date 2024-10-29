using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using UnityEngine;
using VContainer;

namespace Game
{
    public class GuildBankContainer : UIContainer
    {
        [Header("Characters")]
        [SerializeField] private CharactersScrollView charactersScroll;

        [Header("Bunk Tabs")]
        [SerializeField] private GuildBankTabsContainer bankTabsContainer;

        private CharactersVM charactersVM;
        private GuildBankTabsVM bankTabsVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            bankTabsVM = guildVMF.GetGuildBankTabs();
            charactersVM = guildVMF.GetRoster();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            var hasBack = parameters.HasBackRouteKey();
            var hasReinit = parameters.HasReinitializeKey();
            var forcedReset = hasReinit && !hasBack;

            bankTabsVM.AddTo(disp);
            charactersVM.AddTo(disp);

            charactersScroll.Init(charactersVM, forcedReset);
            bankTabsContainer.Init(bankTabsVM, disp, forcedReset);
        }
    }
}