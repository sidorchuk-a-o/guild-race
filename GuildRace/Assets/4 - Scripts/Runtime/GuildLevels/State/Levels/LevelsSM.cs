using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.GuildLevels
{
    [JsonObject(MemberSerialization.Fields)]
    public class LevelsSM
    {
        [ES3Serializable] private List<LevelSM> values;

        public LevelsSM(IEnumerable<LevelInfo> values)
        {
            this.values = values
                .Select(x => new LevelSM(x))
                .ToList();
        }

        public IEnumerable<LevelInfo> GetValues(GuildLevelsConfig config, IObjectResolver resolver)
        {
            var levelsCount = config.Levels.Count;

            for (var i = 0; i < levelsCount; i++)
            {
                var levelData = config.Levels[i];
                var levelSM = values.FirstOrDefault(x => x.Id == levelData.Id);
                var level = i + 1;

                resolver.Inject(levelData.Mechanic);

                if (levelSM != null)
                {
                    yield return levelSM.GetValue(level, levelData);
                }
                else
                {
                    var levelInfo = new LevelInfo(level, levelData);

                    if (level == 1)
                    {
                        levelInfo.MarkAsReadyUnlock();
                    }

                    yield return levelInfo;
                }
            }

            yield break;
        }
    }
}