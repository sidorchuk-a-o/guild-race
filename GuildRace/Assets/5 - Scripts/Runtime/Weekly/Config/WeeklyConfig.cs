using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Weekly
{
    public class WeeklyConfig : ScriptableConfig
    {
        [SerializeField] private int daysCount;

        public int DaysCount => daysCount;
    }
}