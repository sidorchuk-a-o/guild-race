using AD.Services.ProtectedTime;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Instances
{
    public class TimerVM : ViewModel
    {
        private readonly long completeTime;
        private readonly ReactiveProperty<string> value = new();

        private readonly ITimeService timeService;

        public IReadOnlyReactiveProperty<string> Value => value;

        public TimerVM(long completeTime, ITimeService timeService)
        {
            this.completeTime = completeTime;
            this.timeService = timeService;
        }

        protected override void InitSubscribes()
        {
            timeService.OnTick
                .Subscribe(TickCallback)
                .AddTo(this);
        }

        private void TickCallback(TimeTick tick)
        {
            var timeLeft = completeTime > timeService.TotalTime
                ? completeTime - timeService.TotalTime
                : 0;

            value.Value = timeLeft.SecondsToDDHHMMSS();
        }
    }
}