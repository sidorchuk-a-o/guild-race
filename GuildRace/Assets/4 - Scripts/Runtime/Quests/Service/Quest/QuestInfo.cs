using AD.Services.Store;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Quests
{
    public class QuestInfo
    {
        private readonly ReactiveProperty<int> progress = new();
        private readonly ReactiveProperty<bool> isCompleted = new();
        private readonly ReactiveProperty<bool> isRewarded = new();

        public string Id { get; }
        public int DataId { get; }

        public QuestsGroup GroupId { get; }
        public int MechanicId { get; }
        public IReadOnlyList<string> MechanicParams { get; }
        public CurrencyAmount Reward { get; }

        public int RequiredProgress { get; }
        public IReadOnlyReactiveProperty<int> ProgressCounter => progress;

        public IReadOnlyReactiveProperty<bool> IsCompleted => isCompleted;
        public IReadOnlyReactiveProperty<bool> IsRewarded => isRewarded;

        public QuestInfo(string id, QuestData data)
        {
            Id = id;
            DataId = data.Id;
            GroupId = data.GroupId;
            MechanicId = data.MechanicId;
            MechanicParams = data.MechanicParams;
            RequiredProgress = data.RequiredProgress;
            Reward = data.Reward;
        }

        public void AddProgress(int value)
        {
            SetProgress(progress.Value + value);
        }

        public void SetProgress(int value)
        {
            var newProgress = Mathf.Clamp(value, 0, RequiredProgress);

            progress.Value = newProgress;
            isCompleted.Value = newProgress >= RequiredProgress;
        }

        public void MarkAsRewarded()
        {
            isRewarded.Value = true;
        }
    }
}