using AD.Services.Store;
using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Quests
{
    [Serializable]
    public class QuestData : Entity<int>
    {
        [SerializeField] private QuestsGroup groupId;
        [SerializeField] private int mechanicId;
        [SerializeField] private int requiredProgress;
        [SerializeField] private List<string> mechanicParams;
        [SerializeField] private CurrencyAmountData reward;

        public QuestsGroup GroupId => groupId;
        public int MechanicId => mechanicId;
        public int RequiredProgress => requiredProgress;
        public IReadOnlyList<string> MechanicParams => mechanicParams;
        public CurrencyAmount Reward => reward;
    }
}