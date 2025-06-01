using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Craft;
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
        [SerializeField] private RecycleSlotContainer recycleSlot;

        private CharactersVM charactersVM;
        private GuildBankTabsVM bankTabsVM;
        private RecycleSlotVM recycleSlotVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF, CraftVMFactory craftVMF)
        {
            bankTabsVM = guildVMF.GetGuildBankTabs();
            charactersVM = guildVMF.GetRoster();
            recycleSlotVM = craftVMF.GetRecycleSlot();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            var hasForcedReset = parameters.HasForceReset();

            bankTabsVM.AddTo(disp);
            charactersVM.AddTo(disp);
            recycleSlotVM.AddTo(disp);

            charactersScroll.Init(charactersVM, hasForcedReset);
            bankTabsContainer.Init(bankTabsVM, disp);
            recycleSlot.Init(recycleSlotVM, disp);
        }
    }
}