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
        [SerializeField] private RemoveItemSlotContainer removeItemSlot;

        private CharactersVM charactersVM;
        private GuildBankTabsVM bankTabsVM;
        private RemoveItemSlotVM removeItemSlotVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF, CraftVMFactory craftVMF)
        {
            bankTabsVM = guildVMF.GetGuildBankTabs();
            charactersVM = guildVMF.GetRoster();
            removeItemSlotVM = craftVMF.GetRemoveItemSlot();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            var hasForcedReset = parameters.HasForceReset();

            bankTabsVM.AddTo(disp);
            charactersVM.AddTo(disp);
            removeItemSlotVM.AddTo(disp);

            charactersScroll.Init(charactersVM, hasForcedReset);
            bankTabsContainer.Init(bankTabsVM, disp, hasForcedReset);
            removeItemSlot.Init(removeItemSlotVM, disp);
        }
    }
}