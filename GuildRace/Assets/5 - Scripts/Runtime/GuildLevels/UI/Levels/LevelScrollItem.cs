using System.Threading;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

namespace AD.Game.GuildLevels
{
    public class LevelScrollItem : VMScrollItem<LevelVM>
    {
        [Header("Level")]
        [SerializeField] private UIText levelText;
        [SerializeField] private UIText descText;
        [SerializeField] private UnlockLevelButton unlockButton;
        [Space]
        [SerializeField] private UIStates unlockedState;
        [SerializeField] private string unlockedKey = "unlocked";

        private IGuildLevelsService guildLevelsService;

        [Inject]
        public void Inject(IGuildLevelsService guildLevelsService)
        {
            this.guildLevelsService = guildLevelsService;
        }

        protected override void Awake()
        {
            base.Awake();

            unlockButton.OnUnlock
                .Subscribe(UnlockCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            levelText.SetTextParams(ViewModel.Level);
            descText.SetTextParams(ViewModel.DescData);

            ViewModel.IsUnlocked
                .Subscribe(UnlockedChangedCallback)
                .AddTo(disp);

            await unlockButton.Init(ViewModel, disp, ct);
        }

        private void UnlockedChangedCallback(bool state)
        {
            var stateKey = !state
                ? UISelectable.DefaultStateKey
                : unlockedKey;

            unlockedState.SetState(stateKey);
        }

        private void UnlockCallback()
        {
            guildLevelsService.UnlockLevel(ViewModel.Id);
        }
    }
}