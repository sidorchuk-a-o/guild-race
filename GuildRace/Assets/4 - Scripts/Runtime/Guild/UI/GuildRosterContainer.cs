using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using DG.Tweening;
using Game.Inventory;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private NicknameComponent nicknameComponent;
        [SerializeField] private UIText classNameText;
        [SerializeField] private UIText specNameText;
        [SerializeField] private UIText guildRankText;
        [SerializeField] private UIDropdown guildRankDropdown;
        [Space]
        [SerializeField] private ClassContainer classContainer;
        [SerializeField] private AbilitiesContainer abilitiesContainer;
        [SerializeField] private ItemSlotsContainer equipSlotsContainer;

        private readonly CompositeDisp characterDisp = new();
        private CancellationTokenSource characterToken;

        private GuildVMFactory guildVMF;

        private string lastCharacterId;
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
            removeButton.OnClick
                .Subscribe(RemoveCharacterCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            var hasBack = parameters.HasBackRouteKey();
            var hasForcedReset = parameters.HasForceReset();

            charactersVM.AddTo(disp);
            charactersScroll.Init(charactersVM, hasForcedReset);

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
                    characterContainer.SetInteractable(false);
                }

                return;
            }

            InitGuildRankDropdown();

            SelectCharacter(characterVM ?? charactersVM.FirstOrDefault());
        }

        private void InitGuildRankDropdown()
        {
            var guildRankOptions = guildVMF.GetGuildRanksOptions();

            guildRankDropdown.SetOptions(guildRankOptions);
        }

        private void CharacterSelectCallback(CharacterVM characterVM)
        {
            SelectCharacter(characterVM);
        }

        private void SelectCharacter(CharacterVM characterVM)
        {
            this.characterVM?.SetSelectState(false);

            this.characterVM = characterVM;
            this.characterVM?.SetSelectState(true);

            UpdateCharacterBlock();
        }

        private async void UpdateCharacterBlock()
        {
            characterDisp.Clear();
            characterDisp.AddTo(disp);

            var hasCharacter = characterVM != null;
            var token = new CancellationTokenSource();

            characterToken?.Cancel();
            characterToken = token;

            if (hasCharacter &&
                lastCharacterId.IsValid() &&
                lastCharacterId == characterVM.Id)
            {
                updateCharacter();
                return;
            }

            lastCharacterId = characterVM?.Id;

            characterContainer.DOKill();
            characterContainer.SetInteractable(hasCharacter);

            const float duration = 0.1f;

            await characterContainer.DOFade(0, duration);

            if (hasCharacter == false || token.IsCancellationRequested)
            {
                return;
            }

            updateCharacter();

            await characterContainer.DOFade(1, duration);

            void updateCharacter()
            {
                nicknameComponent.Init(characterVM);
                classNameText.SetTextParams(characterVM.ClassVM.NameKey);
                specNameText.SetTextParams(characterVM.SpecVM.NameKey);

                classContainer.Init(characterVM, token);
                abilitiesContainer.Init(characterVM.SpecVM.AbilitiesVM, token);
                equipSlotsContainer.Init(characterVM.EquiSlotsVM, characterDisp);

                characterVM.ItemsLevel
                    .Subscribe(x => itemsLevelText.SetTextParams(x))
                    .AddTo(characterDisp);

                characterVM.GuildRankName
                    .Subscribe(x => guildRankText.SetTextParams(x))
                    .AddTo(characterDisp);

                characterVM.InstanceVM
                    .Subscribe(x => removeButton.SetInteractableState(!characterVM.HasInstance))
                    .AddTo(characterDisp);

                var guildRankId = characterVM.GuildRankVM.Value.Id;
                var guildRankIndex = guildVMF.GetGuildRankIndex(guildRankId);
                guildRankDropdown.SetValue(guildRankIndex - 1);

                guildRankDropdown.Value
                    .SilentSubscribe(guildRankChangedCallback)
                    .AddTo(characterDisp);

                void guildRankChangedCallback(int index)
                {
                    var characterId = characterVM.Id;

                    guildVMF.UpdateGuildRank(characterId, index + 1);

                    var newCharacterVM = charactersVM.FirstOrDefault(x => x.Id == characterVM.Id);

                    SelectCharacter(newCharacterVM);
                }
            }
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

            Router.Push(RouteKeys.Guild.RemoveCharacter, parameters: parameters);
        }
    }
}