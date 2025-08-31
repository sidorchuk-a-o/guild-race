using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Game.GuildLevels
{
    public class GuildLevelsContainer : UIContainer
    {
        [SerializeField] private LevelsScrollView levelsScroll;

        private LevelsVM levelsVM;

        [Inject]
        public void Inject(GuildLevelsVMFactory guildLevelsVMF)
        {
            levelsVM = guildLevelsVMF.GetGuildLevels();
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            levelsVM.AddTo(disp);

            InitLevels();
        }

        public async void InitLevels()
        {
            await levelsScroll.InitAsync(levelsVM, true);

            levelsScroll.ScrollTo(levelsVM.LastReadyToCompleteIndex, .1f);
        }
    }
}