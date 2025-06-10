using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;

namespace Game.Quests
{
    public class QuestViewItem : MonoBehaviour
    {
        [Header("Quest")]
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText progressText;
        [SerializeField] private GameObject completedIndicator;
        [SerializeField] private GameObject rewardedIndicator;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Buttons")]
        [SerializeField] private UIButton selectButton;
        [SerializeField] private string unselectedStateKey = "default";
        [SerializeField] private string selectedStateKey = "selected";

        private QuestVM questVM;

        private readonly Subject<QuestVM> onSelect = new();

        public IObservable<QuestVM> OnSelect => onSelect;

        private void Awake()
        {
            selectButton.OnClick
                .Subscribe(() => onSelect.OnNext(questVM))
                .AddTo(this);
        }

        public void Init(QuestVM questVM, CompositeDisp disp)
        {
            this.questVM = questVM;

            nameText.SetTextParams(questVM.NameKey);

            questVM.ProgressStr
                .Subscribe(x => progressText.SetTextParams(x))
                .AddTo(disp);

            questVM.IsCompleted
                .SilentSubscribe(UpdateState)
                .AddTo(disp);

            questVM.IsRewarded
                .SilentSubscribe(UpdateState)
                .AddTo(disp);

            questVM.IsSelected
                .Subscribe(SelectedStateChanged)
                .AddTo(disp);

            UpdateState();
        }

        private void UpdateState()
        {
            var isCompleted = questVM.IsCompleted.Value;
            var isRewarded = questVM.IsRewarded.Value;

            completedIndicator.SetActive(isCompleted && !isRewarded);
            rewardedIndicator.SetActive(isRewarded);

            canvasGroup.alpha = isRewarded ? 0.1f : 1;
        }

        private void SelectedStateChanged(bool state)
        {
            if (selectButton != null)
            {
                var stateKey = state
                    ? selectedStateKey
                    : unselectedStateKey;

                selectButton.SetState(stateKey);
            }
        }
    }
}