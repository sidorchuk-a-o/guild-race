#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Guild
{
    public class JoinRequestScrollItem : VMScrollItem<JoinRequestVM>
    {
        [Header("Character")]
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [Space]
        [SerializeField] private NicknameComponent nicknameComponent;

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

            nicknameComponent.Init(characterVM);
            itemsLevelText.SetTextParams(characterVM.ItemsLevel.Value);
            classNameText.SetTextParams(characterVM.ClassVM.NameKey);
            specNameText.SetTextParams(characterVM.SpecVM.NameKey);

            declineButton.SetInteractableState(ViewModel.IsDefault == false);

            characterVM.ItemsLevel
                .Subscribe(x => itemsLevelText.SetTextParams(x))
                .AddTo(disp);

            guildVM.RosterIsFull
                .Subscribe(x => acceptButton.SetInteractableState(x == false))
                .AddTo(disp);
        }

        private void AcceptCallback()
        {
            var requestId = ViewModel.Id;
            var parameters = RouteParams.Default;

            parameters[BaseDialog.okKey] = (Action)(() =>
            {
                guildVMF.AcceptJoinRequest(requestId);
            });

            router.Push(RouteKeys.Guild.AcceptJointRequest, parameters: parameters);
        }

        private void DeclineCallback()
        {
            var requestId = ViewModel.Id;
            var parameters = RouteParams.Default;

            parameters[BaseDialog.okKey] = (Action)(() =>
            {
                guildVMF.DeclineJoinRequest(requestId);
            });

            router.Push(RouteKeys.Guild.DeclineJointRequest, parameters: parameters);
        }
    }
}