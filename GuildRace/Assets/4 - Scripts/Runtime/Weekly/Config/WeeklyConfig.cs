using AD.ToolsCollection;
using UnityEngine;

namespace Game.Weekly
{
    public class WeeklyConfig : ScriptableConfig
    {
        [SerializeField] private int daysCount;

        public int DaysCount => daysCount;
    }
}