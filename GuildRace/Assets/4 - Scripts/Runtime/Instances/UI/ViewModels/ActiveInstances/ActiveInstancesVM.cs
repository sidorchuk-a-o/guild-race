using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Instances
{
    public class ActiveInstancesVM : VMReactiveCollection<ActiveInstanceInfo, ActiveInstanceVM>
    {
        private readonly InstancesVMFactory instancesVMF;
        private readonly ReactiveProperty<int> completedCount = new();

        public IReadOnlyReactiveProperty<int> CompletedCount => completedCount;

        public ActiveInstancesVM(IActiveInstancesCollection values, InstancesVMFactory instancesVMF)
            : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override ActiveInstanceVM Create(ActiveInstanceInfo value)
        {
            return new ActiveInstanceVM(value, instancesVMF);
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
                if (GetOrCreate(i).IsReadyToComplete.Value)
                {
                    count++;
                }
            }

            completedCount.Value = count;
        }
    }
}