using System.Threading;
using AD.Game.Guild;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

namespace AD.Game
{
    public class GuildLoadingScreen : UIContainer
    {
        [SerializeField] private UIText headerText;

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

            guildVM.GuildName
                .Subscribe(x => headerText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}