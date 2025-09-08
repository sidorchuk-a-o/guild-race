using UniRx;

namespace Game.Weekly
{
    public interface IWeeklyService
    {
        IReadOnlyReactiveProperty<int> CurrentWeek { get; }
        IReadOnlyReactiveProperty<int> CurrentDayOfWeek { get; }
    }
}