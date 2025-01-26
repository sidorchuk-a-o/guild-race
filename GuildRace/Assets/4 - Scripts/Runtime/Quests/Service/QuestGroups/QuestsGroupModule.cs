using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;

namespace Game.Quests
{
    public abstract class QuestsGroupModule : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private int maxQuestsCount;

        protected QuestsConfig questsConfig;
        protected IObjectResolver resolver;

        public LocalizeKey NameKey => nameKey;
        public int MaxQuestsCount => maxQuestsCount;

        public abstract IQuestsCollection Quests { get; }

        [Inject]
        public void Inject(QuestsConfig questsConfig, IObjectResolver resolver)
        {
            this.questsConfig = questsConfig;
            this.resolver = resolver;
        }

        public virtual void Init()
        {
        }
    }
}