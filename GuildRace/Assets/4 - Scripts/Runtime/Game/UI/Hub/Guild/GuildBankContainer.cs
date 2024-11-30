using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Game.Guild
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

            bankTabsVM.AddTo(disp);
            charactersVM.AddTo(disp);

            var hasForcedReset = parameters.HasForceReset();

            charactersScroll.Init(charactersVM, hasForcedReset);
            bankTabsContainer.Init(bankTabsVM, disp, hasForcedReset);
        }
    }
}