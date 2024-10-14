using UniRx;

namespace Game.Guild
{
    public class ClassWeightInfo
    {
        private readonly ReactiveProperty<bool> isEnabled = new();

        public ClassId ClassId { get; }
        public IReadOnlyReactiveProperty<bool> IsEnabled => isEnabled;

        public ClassWeightInfo(ClassId classId, bool isEnabled)
        {
            ClassId = classId;

            this.isEnabled.Value = isEnabled;
        }

        public void SetActiveState(bool value)
        {
            isEnabled.Value = value;
        }
    }
}