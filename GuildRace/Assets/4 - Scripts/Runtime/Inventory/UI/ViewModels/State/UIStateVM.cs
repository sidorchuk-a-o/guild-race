using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Inventory
{
    public class UIStateVM : ViewModel
    {
        private const string defaultState = "default";

        private readonly ReactiveProperty<string> value;

        public IReadOnlyReactiveProperty<string> Value => value;

        public UIStateVM()
        {
            value = new(defaultState);
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
        }

        public void SetState(string value)
        {
            this.value.Value = value.IsNullOrEmpty() ? defaultState : value;
        }

        public void ResetState()
        {
            SetState(defaultState);
        }
    }
}