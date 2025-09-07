using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using Game.Guild;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using UniRx;

namespace Game
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