using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Inventory;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Guild
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
        [SerializeField] private ClassContainer classContainer;
        [SerializeField] private AbilitiesContainer abilitiesContainer;
        [SerializeField] private ItemSlotsContainer equipSlotsContainer;

        [Header("Settings")]
        [SerializeField] private UIButton settingsButton;

        private readonly CompositeDisp requestDisp = new();
        private CancellationTokenSource requestToken;

        private string lastRequestId;
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

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            var hasBack = parameters.HasBackRouteKey();
            var hasForcedReset = parameters.HasForceReset();

            joinRequestsVM.AddTo(disp);
            joinRequestsScroll.Init(joinRequestsVM, hasForcedReset);

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

            SelectRequest(joinRequestVM ?? joinRequestsVM.FirstOrDefault());
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
            this.joinRequestVM?.SetSelectState(false);

            this.joinRequestVM = joinRequestVM;
            this.joinRequestVM?.SetSelectState(true);

            UpdateCharacterBlock();
        }

        private async void UpdateCharacterBlock()
        {
            requestDisp.Clear();
            requestDisp.AddTo(disp);

            var hasRequest = joinRequestVM != null;
            var token = new CancellationTokenSource();

            requestToken?.Cancel();
            requestToken = token;

            if (hasRequest &&
                lastRequestId.IsValid() &&
                lastRequestId == joinRequestVM.Id)
            {
                updateCharacter();
                return;
            }

            lastRequestId = joinRequestVM?.Id;

            characterContainer.DOKill();
            characterContainer.interactable = hasRequest;

            const float duration = 0.1f;

            await characterContainer.DOFade(0, duration);

            if (hasRequest == false || token.IsCancellationRequested)
            {
                return;
            }

            updateCharacter();

            await characterContainer.DOFade(1, duration);

            void updateCharacter()
            {
                var characterVM = joinRequestVM.CharacterVM;

                nicknameText.SetTextParams(characterVM.Nickname);
                classNameText.SetTextParams(characterVM.ClassVM.NameKey);
                specNameText.SetTextParams(characterVM.SpecVM.NameKey);

                classContainer.Init(characterVM, token);
                abilitiesContainer.Init(characterVM.SpecVM.AbilitiesVM, token);
                equipSlotsContainer.Init(characterVM.EquiSlotsVM, requestDisp);

                characterVM.ItemsLevel
                    .Subscribe(x => itemsLevelText.SetTextParams(x))
                    .AddTo(requestDisp);
            }
        }

        private void SettingsButtonCallback()
        {
            Router.Push(RouteKeys.Guild.RecruitingSettings);
        }
    }
}