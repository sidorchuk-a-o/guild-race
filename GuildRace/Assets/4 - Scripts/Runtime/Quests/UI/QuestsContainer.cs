﻿using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Quests
{
    public class QuestsContainer : UIContainer
    {
        [Header("Quests")]
        [SerializeField] private QuestGroupViewParams[] groupParams;

        [Header("Quest")]
        [SerializeField] private CanvasGroup questContainer;
        [SerializeField] private CanvasGroup emptyQuestContainer;
        [Space]
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText descText;
        [SerializeField] private QuestProgressContainer progressContainer;
        [SerializeField] private UIText rewardAmountText;
        [Space]
        [SerializeField] private UIButton takeRewardButton;
        [SerializeField] private GameObject rewardedPlug;

        private readonly CompositeDisp questDisp = new();
        private CancellationTokenSource questToken;

        private QuestsVMFactory questsVMF;

        private string lastQuestId;
        private QuestVM questVM;
        private QuestsVM[] questsVMs;

        [Inject]
        public void Inject(QuestsVMFactory questsVMF)
        {
            this.questsVMF = questsVMF;

            questsVMs = groupParams
                .Select(x => questsVMF.GetQuests(x.Group))
                .ToArray();
        }

        private void Awake()
        {
            questContainer.alpha = 0;
            questContainer.interactable = false;

            takeRewardButton.OnClick
                .Subscribe(TakeRewardCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            var hasForcedReset = parameters.HasForceReset();

            questsVMs.ForEach(x => x.AddTo(disp));

            for (var i = 0; i < questsVMs.Length; i++)
            {
                var questsVM = questsVMs[i];
                var questsView = groupParams[i].QuestsView;

                questsView.Init(questsVM, disp);

                questsView.OnSelect
                    .Subscribe(QuestSelectCallback)
                    .AddTo(disp);
            }

            UpdateQuestBlock();
        }

        private void QuestSelectCallback(QuestVM questVM)
        {
            SelectQuest(questVM);
        }

        private void SelectQuest(QuestVM questVM)
        {
            this.questVM?.SetSelectState(false);

            this.questVM = questVM;
            this.questVM?.SetSelectState(true);

            UpdateQuestBlock();
        }

        private async void UpdateQuestBlock()
        {
            questDisp.Clear();
            questDisp.AddTo(disp);

            var hasQuest = questVM != null;
            var token = new CancellationTokenSource();

            questToken?.Cancel();
            questToken = token;

            if (hasQuest == false &&
                lastQuestId.IsValid() == false)
            {
                return;
            }

            if (hasQuest &&
                lastQuestId.IsValid() &&
                lastQuestId == questVM.Id)
            {
                updateQuest();
                return;
            }

            lastQuestId = questVM?.Id;

            questContainer.DOKill();
            emptyQuestContainer.DOKill();

            questContainer.interactable = hasQuest;
            emptyQuestContainer.interactable = !hasQuest;

            const float duration = 0.1f;

            await UniTask.WhenAll(
                questContainer.DOFade(0, duration).ToUniTask(),
                emptyQuestContainer.DOFade(0, duration).ToUniTask());

            if (token.IsCancellationRequested)
            {
                return;
            }

            updateQuest();

            var showContainer = hasQuest
                ? questContainer
                : emptyQuestContainer;

            await showContainer.DOFade(1, duration);

            void updateQuest()
            {
                if (hasQuest)
                {
                    nameText.SetTextParams(questVM.NameKey);
                    descText.SetTextParams(questVM.DescKey);

                    rewardAmountText.SetTextParams((long)questVM.Reward.Value);

                    progressContainer.Init(questVM, disp);

                    questVM.IsCompleted
                        .SilentSubscribe(UpdateButtons)
                        .AddTo(disp);

                    questVM.IsRewarded
                        .SilentSubscribe(UpdateButtons)
                        .AddTo(disp);

                    UpdateButtons();
                }
            }
        }

        private void UpdateButtons()
        {
            var isCompleted = questVM.IsCompleted.Value;
            var isRewarded = questVM.IsRewarded.Value;

            takeRewardButton.SetInteractableState(isCompleted);
            takeRewardButton.SetActive(!isRewarded);

            rewardedPlug.SetActive(isCompleted && isRewarded);
        }

        private void TakeRewardCallback()
        {
            if (questVM.IsCompleted.Value)
            {
                var args = new TakeRewardArgs
                {
                    GroupId = questVM.GroupId,
                    QuestId = questVM.Id
                };

                questsVMF.TakeQuestReward(args);
            }
        }
    }
}