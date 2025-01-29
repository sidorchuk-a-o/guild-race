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

        [Header("Buttons")]
        [SerializeField] private UIButton selectButton;

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
                .Subscribe(x => completedIndicator.SetActive(x))
                .AddTo(disp);

            questVM.IsRewarded
                .Subscribe(x => rewardedIndicator.SetActive(x))
                .AddTo(disp);
        }
    }
}