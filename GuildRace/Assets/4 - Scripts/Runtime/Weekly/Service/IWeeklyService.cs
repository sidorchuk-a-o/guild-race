namespace Game.Weekly
{
    public interface IWeeklyService
    {
        int CurrentWeek { get; }
        int CurrentDayOfWeek { get; }
    }
}