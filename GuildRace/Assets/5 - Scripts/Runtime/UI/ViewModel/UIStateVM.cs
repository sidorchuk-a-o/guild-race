using AD.Router;
using AD.ToolsCollection;
using R3;

namespace AD.Game.UI
{
    public class UIStateVM : ViewModel
    {
        private const string DefaultState = "default";

        private readonly ReactiveProperty<string> value;

        public ReadOnlyReactiveProperty<string> Value => value;

        public UIStateVM()
        {
            value = new(DefaultState);
        }

        protected override void InitSubscribes()
        {
            ResetState();
        }

        public override void ResetSubscribes()
        {
            ResetState();

            base.ResetSubscribes();
        }

        public void SetState(string value)
        {
            this.value.Value = value.IsNullOrEmpty() ? DefaultState : value;
        }

        public void ResetState()
        {
            SetState(DefaultState);
        }
    }
}