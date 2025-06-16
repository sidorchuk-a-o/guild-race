using AD.Services.ProtectedTime;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Instances
{
    public class TimerVM : ViewModel
    {
        private readonly long startTime;
        private readonly long timerTime;
        private readonly long completeTime;
        private readonly ITimeService timeService;

        private readonly ReactiveProperty<float> progress = new();
        private readonly ReactiveProperty<long> timeLeft = new();
        private readonly ReactiveProperty<string> timeLeftStr = new();

        public IReadOnlyReactiveProperty<float> Progress => progress;
        public IReadOnlyReactiveProperty<long> TimeLeft => timeLeft;
        public IReadOnlyReactiveProperty<string> TimeLeftStr => timeLeftStr;

        public TimerVM(long startTime, long timerTime, ITimeService timeService)
        {
            this.startTime = startTime;
            this.timerTime = timerTime;
            this.timeService = timeService;

            completeTime = startTime + timerTime;
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

            var timeLeftStr = timeLeft.SecondsToDDHHMMSS();

            var progress = 1 - (float)timeLeft / timerTime;

            this.progress.Value = progress;
            this.timeLeft.Value = timeLeft;
            this.timeLeftStr.Value = timeLeftStr;
        }
    }
}