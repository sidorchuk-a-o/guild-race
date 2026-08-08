using R3;

namespace AD.Game.Weekly
{
    public interface IWeeklyService
    {
        ReadOnlyReactiveProperty<int> CurrentWeek { get; }
        ReadOnlyReactiveProperty<int> CurrentDayOfWeek { get; }
    }
}