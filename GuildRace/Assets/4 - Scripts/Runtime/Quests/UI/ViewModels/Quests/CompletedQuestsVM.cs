using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;

namespace Game.Quests
{
    public class CompletedQuestsVM : ViewModel
    {
        private readonly IQuestsService questsService;
        private readonly ReactiveProperty<int> count = new();

        public IReadOnlyReactiveProperty<int> Count => count;

        public CompletedQuestsVM(IQuestsService questsService)
        {
            this.questsService = questsService;
        }

        protected override void InitSubscribes()
        {
            var completedCount = 0;
            var quests = questsService.Modules.SelectMany(x => x.Quests);

            foreach (var quest in quests)
            {
                quest.IsCompleted
                    .SilentSubscribe(CompletedCallback)
                    .AddTo(this);

                quest.IsRewarded
                    .SilentSubscribe(RewardedCallback)
                    .AddTo(this);

                if (quest.IsCompleted.Value && !quest.IsRewarded.Value)
                {
                    completedCount++;
                }
            }

            count.Value = completedCount;
        }

        private void CompletedCallback()
        {
            count.Value++;
        }

        private void RewardedCallback()
        {
            count.Value--;
        }
    }
}