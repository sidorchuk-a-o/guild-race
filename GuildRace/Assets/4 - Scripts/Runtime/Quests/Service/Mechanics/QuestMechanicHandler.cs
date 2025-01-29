using AD.Services.Localization;
using AD.ToolsCollection;
using AD.UI;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Quests
{
    public abstract class QuestMechanicHandler : ScriptableEntity<int>
    {
        [SerializeField] protected LocalizeKey nameKey;
        [SerializeField] protected LocalizeKey descKey;

        private IQuestsService questsService;

        protected IEnumerable<QuestInfo> Quests => questsService.Quests.Where(x =>
        {
            return x.MechanicId == Id
                && x.IsCompleted.Value == false;
        });

        [Inject]
        public void Inject(IQuestsService questsService)
        {
            this.questsService = questsService;
        }

        public virtual void Init()
        {
        }

        public virtual UITextData GetNameKey(QuestInfo quest)
        {
            return nameKey;
        }

        public virtual UITextData GetDescKey(QuestInfo quest)
        {
            return descKey;
        }
    }
}