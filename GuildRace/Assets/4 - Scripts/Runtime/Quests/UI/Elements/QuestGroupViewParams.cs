using System;
using UnityEngine;

namespace Game.Quests
{
    [Serializable]
    public class QuestGroupViewParams
    {
        [SerializeField] private QuestsGroup group;
        [SerializeField] private QuestsViewContainer questsView;

        public QuestsGroup Group => group;
        public QuestsViewContainer QuestsView => questsView;
    }
}