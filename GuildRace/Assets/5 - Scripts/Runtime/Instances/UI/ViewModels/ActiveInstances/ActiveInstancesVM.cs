using AD.Router;
using AD.ToolsCollection;
using R3;

namespace AD.Game.Instances
{
    public class ActiveInstancesVM : VMReactiveCollection<ActiveInstanceInfo, ActiveInstanceVM>
    {
        private readonly InstancesVMFactory instancesVMF;
        private readonly ReactiveProperty<int> completedCount = new();

        public ReadOnlyReactiveProperty<int> CompletedCount => completedCount;

        public ActiveInstancesVM(IActiveInstancesCollection values, InstancesVMFactory instancesVMF)
            : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override ActiveInstanceVM Create(ActiveInstanceInfo value)
        {
            return new(value, instancesVMF);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            for (var i = 0; i < Count; i++)
            {
                GetOrCreate(i)
                    .IsReadyToComplete
                    .Subscribe(UpdateCompletedCount)
                    .AddTo(this);
            }

            ObserveAdd()
                .Subscribe(x =>
                {
                    GetOrCreate(x.Index)
                        .IsReadyToComplete
                        .Subscribe(UpdateCompletedCount)
                        .AddTo(this);
                })
                .AddTo(this);

            ObserveRemove()
                .Subscribe(UpdateCompletedCount)
                .AddTo(this);

            UpdateCompletedCount();
        }

        private void UpdateCompletedCount()
        {
            var count = 0;

            for (var i = 0; i < Count; i++)
            {
                if (GetOrCreate(i).IsReadyToComplete.CurrentValue)
                {
                    count++;
                }
            }

            completedCount.Value = count;
        }
    }
}