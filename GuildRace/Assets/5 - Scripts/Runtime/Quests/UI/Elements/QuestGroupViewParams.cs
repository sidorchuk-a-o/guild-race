using System;
using UnityEngine;

namespace AD.Game.Quests
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