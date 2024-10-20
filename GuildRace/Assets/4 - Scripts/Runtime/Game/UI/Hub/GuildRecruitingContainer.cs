using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Guild;
using Game.Items;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game
{
    public class GuildRecruitingContainer : UIContainer
    {
        [Header("Requests")]
        [SerializeField] private JoinRequestsScrollView joinRequestsScroll;

        [Header("Character")]
        [SerializeField] private CanvasGroup characterContainer;
        [Space]
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText nicknameText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [Space]
        [SerializeField] private EquipSlotsContainer equipSlotsContainer;

        [Header("Settings")]
        [SerializeField] private UIButton settingsButton;

        private readonly CompositeDisp requestDisp = new();
        private CancellationTokenSource requestToken;

        private JoinRequestVM joinRequestVM;
        private JoinRequestsVM joinRequestsVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            joinRequestsVM = guildVMF.GetJoinRequests();
        }

        private void Awake()
        {
            characterContainer.alpha = 0;
            characterContainer.interactable = false;

            settingsButton.OnClick
                .Subscribe(SettingsButtonCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            var hasBack = parameters.HasBackRouteKey();
            var hasReinit = parameters.HasReinitializeKey();

            joinRequestsVM.AddTo(disp);
            joinRequestsScroll.Init(joinRequestsVM, forcedReset: hasReinit && !hasBack);

            joinRequestsScroll.OnSelect
                .Subscribe(RequestSelectCallback)
                .AddTo(disp);

            joinRequestsVM.ObserveAdd()
                .Subscribe(AddRequestCallback)
                .AddTo(disp);

            joinRequestsVM.ObserveRemove()
                .Subscribe(RemoveRequestCallback)
                .AddTo(disp);

            joinRequestsVM.ObserveClear()
                .Subscribe(ClearRequestsCallback)
                .AddTo(disp);

            if (hasBack)
            {
                if (joinRequestVM == null)
                {
                    characterContainer.alpha = 0;
                    characterContainer.interactable = false;
                }

                return;
            }

            SelectRequest(joinRequestsVM.FirstOrDefault());
        }

        private void AddRequestCallback(VMAddEvent args)
        {
            if (joinRequestsVM.Count == 1)
            {
                joinRequestVM = null;

                SelectRequest(joinRequestsVM.FirstOrDefault());
            }
        }

        private void RemoveRequestCallback(VMRemoveEvent<JoinRequestVM> args)
        {
            if (joinRequestVM == args.Value)
            {
                joinRequestVM = null;

                SelectRequest(joinRequestsVM.NearbyOrDefault(args.Index));

                if (joinRequestVM == null)
                {
                    characterContainer.alpha = 0;
                    characterContainer.interactable = false;
                }
            }
        }

        private void ClearRequestsCallback()
        {
            joinRequestVM = null;

            characterContainer.alpha = 0;
            characterContainer.interactable = false;
        }

        private void RequestSelectCallback(JoinRequestVM joinRequestVM)
        {
            SelectRequest(joinRequestVM);
        }

        private void SelectRequest(JoinRequestVM joinRequestVM)
        {
            if (this.joinRequestVM == joinRequestVM)
            {
                return;
            }

            this.joinRequestVM?.SetSelectState(false);

            this.joinRequestVM = joinRequestVM;
            this.joinRequestVM?.SetSelectState(true);

            UpdateCharacterBlock();
        }

        private async void UpdateCharacterBlock()
        {
            requestDisp.Clear();
            requestDisp.AddTo(disp);

            var token = new CancellationTokenSource();

            requestToken?.Cancel();
            requestToken = token;

            characterContainer.DOKill();
            characterContainer.interactable = joinRequestVM != null;

            const float duration = 0.1f;

            await characterContainer.DOFade(0, duration);

            if (joinRequestVM == null || token.IsCancellationRequested)
            {
                return;
            }

            var characterVM = joinRequestVM.CharacterVM;

            nicknameText.SetTextParams(characterVM.Nickname);
            itemsLevelText.SetTextParams(characterVM.ItemsLevel.Value);
            classNameText.SetTextParams(characterVM.ClassVM.NameKey);
            specNameText.SetTextParams(characterVM.SpecVM.Value.NameKey);

            equipSlotsContainer.Init(characterVM.EquiSlotsVM, requestDisp);

            await characterContainer.DOFade(1, duration);
        }

        private void SettingsButtonCallback()
        {
            Router.Push(RouteKeys.Guild.recruitingSettings);
        }
    }
}