#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game
{
    public class JoinRequestItem : VMScrollItem<JoinRequestVM>
    {
        [Header("Character")]
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [Space]
        [SerializeField] private UIText nicknameText;

        [Header("Buttons")]
        [SerializeField] private UIButton acceptButton;
        [SerializeField] private UIButton declineButton;

        private IRouterService router;

        private GuildVM guildVM;
        private GuildVMFactory guildVMF;

        [Inject]
        public void Inject(IRouterService router, GuildVMFactory guildVMF)
        {
            this.router = router;
            this.guildVMF = guildVMF;

            guildVM = guildVMF.GetGuild();
        }

        protected override void Awake()
        {
            base.Awake();

            acceptButton.OnClick
                .Subscribe(AcceptCallback)
                .AddTo(this);

            declineButton.OnClick
                .Subscribe(DeclineCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            guildVM.AddTo(disp);

            var characterVM = ViewModel.CharacterVM;

            nicknameText.SetTextParams(characterVM.Nickname);
            itemsLevelText.SetTextParams(characterVM.ItemsLevel.Value);
            classNameText.SetTextParams(characterVM.ClassVM.NameKey);
            specNameText.SetTextParams(characterVM.SpecVM.Value.NameKey);

            declineButton.SetInteractableState(ViewModel.IsDefault == false);

            guildVM.RosterIsFull
                .Subscribe(x => acceptButton.SetInteractableState(x == false))
                .AddTo(disp);
        }

        private void AcceptCallback()
        {
            var parameters = RouteParams.Default;

            parameters[BaseDialog.okKey] = (Action)(() =>
            {
                guildVMF.AcceptJoinRequest(ViewModel.Id);
            });

            router.Push(RouteKeys.Guild.acceptJointRequest, parameters: parameters);
        }

        private void DeclineCallback()
        {
            var parameters = RouteParams.Default;

            parameters[BaseDialog.okKey] = (Action)(() =>
            {
                guildVMF.DeclineJoinRequest(ViewModel.Id);
            });

            router.Push(RouteKeys.Guild.declineJointRequest, parameters: parameters);
        }
    }
}