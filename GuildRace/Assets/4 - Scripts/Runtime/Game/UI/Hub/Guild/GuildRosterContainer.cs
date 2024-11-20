using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Inventory;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Guild
{
    public class GuildRosterContainer : UIContainer
    {
        [Header("Characters")]
        [SerializeField] private CharactersScrollView charactersScroll;
        [SerializeField] private UIText charactersCountText;

        [Header("Character")]
        [SerializeField] private CanvasGroup characterContainer;
        [SerializeField] private UIButton removeButton;
        [Space]
        [SerializeField] private UIText itemsLevelText;
        [SerializeField] private UIText nicknameText;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [SerializeField] private UIText guildRankText;
        [Space]
        [SerializeField] private ItemSlotsContainer equipSlotsContainer;

        private readonly CompositeDisp characterDisp = new();
        private CancellationTokenSource characterToken;

        private GuildVMFactory guildVMF;

        private CharacterVM characterVM;
        private CharacterVM switchToCharacterVM;

        private CharactersVM charactersVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            this.guildVMF = guildVMF;

            charactersVM = guildVMF.GetRoster();
        }

        private void Awake()
        {
            characterContainer.alpha = 0;
            characterContainer.interactable = false;

            removeButton.OnClick
                .Subscribe(RemoveCharacterCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            var hasBack = parameters.HasBackRouteKey();
            var hasReinit = parameters.HasReinitializeKey();

            charactersVM.AddTo(disp);
            charactersScroll.Init(charactersVM, forcedReset: hasReinit && !hasBack);

            charactersVM.CountStr
                .Subscribe(x => charactersCountText.SetTextParams(x))
                .AddTo(disp);

            charactersScroll.OnSelect
                .Subscribe(CharacterSelectCallback)
                .AddTo(disp);

            if (hasBack)
            {
                if (switchToCharacterVM != null)
                {
                    SelectCharacter(switchToCharacterVM);

                    switchToCharacterVM = null;
                }

                if (characterVM == null)
                {
                    characterContainer.alpha = 0;
                    characterContainer.interactable = false;
                }

                return;
            }

            SelectCharacter(charactersVM.FirstOrDefault());
        }

        private void CharacterSelectCallback(CharacterVM characterVM)
        {
            SelectCharacter(characterVM);
        }

        private void SelectCharacter(CharacterVM characterVM)
        {
            if (this.characterVM == characterVM)
            {
                return;
            }

            this.characterVM?.SetSelectState(false);

            this.characterVM = characterVM;
            this.characterVM?.SetSelectState(true);

            UpdateCharacterBlock();
        }

        private async void UpdateCharacterBlock()
        {
            characterDisp.Clear();
            characterDisp.AddTo(disp);

            var selected = characterVM != null;
            var token = new CancellationTokenSource();

            characterToken?.Cancel();
            characterToken = token;

            characterContainer.DOKill();
            characterContainer.interactable = selected;

            const float duration = 0.1f;

            await characterContainer.DOFade(0, duration);

            if (selected == false || token.IsCancellationRequested)
            {
                return;
            }

            nicknameText.SetTextParams(characterVM.Nickname);
            classNameText.SetTextParams(characterVM.ClassVM.NameKey);

            characterVM.ItemsLevel
                .Subscribe(x => itemsLevelText.SetTextParams(x))
                .AddTo(characterDisp);

            characterVM.GuildRankName
                .Subscribe(x => guildRankText.SetTextParams(x))
                .AddTo(characterDisp);

            characterVM.SpecVM
                .Subscribe(x => specNameText.SetTextParams(x.NameKey))
                .AddTo(characterDisp);

            equipSlotsContainer.Init(characterVM.EquiSlotsVM, characterDisp);

            await characterContainer.DOFade(1, duration);
        }

        private void RemoveCharacterCallback()
        {
            if (characterVM == null)
            {
                return;
            }

            var parameters = RouteParams.Default;

            parameters[BaseDialog.okKey] = (Action)(() =>
            {
                var index = guildVMF.RemoveCharacter(characterVM.Id);

                characterVM = null;
                switchToCharacterVM = charactersVM.NearbyOrDefault(index);
            });

            Router.Push(RouteKeys.Guild.removeCharacter, parameters: parameters);
        }
    }
}