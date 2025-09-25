using AD.ToolsCollection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Quests
{
    public class QuestsConfig : ScriptableConfig
    {
        [SerializeField] private List<QuestData> quests;
        [Space]
        [SerializeField] private List<QuestsGroupModule> groupModules;
        [SerializeField] private List<QuestMechanicHandler> mechanicHandlers;

        private Dictionary<int, QuestData> questsCache;
        private Dictionary<QuestsGroup, List<QuestData>> questsByGroupCache;

        public IReadOnlyList<QuestData> Quests => quests;
        public IReadOnlyList<QuestsGroupModule> GroupModules => groupModules;
        public IReadOnlyList<QuestMechanicHandler> MechanicHandlers => mechanicHandlers;

        public QuestData GetQuest(int id)
        {
            questsCache ??= quests.ToDictionary(x => x.Id, x => x);
            questsCache.TryGetValue(id, out var data);

            return data;
        }

        public IReadOnlyList<QuestData> GetQuestsByGroup(QuestsGroup group)
        {
            questsByGroupCache ??= quests
                .GroupBy(x => x.GroupId)
                .ToDictionary(x => x.Key, x => x.ToList());

            questsByGroupCache.TryGetValue(group, out var data);

            return data;
        }
    }
}