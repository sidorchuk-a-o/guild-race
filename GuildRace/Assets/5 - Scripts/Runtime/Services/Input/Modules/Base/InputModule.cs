using AD.AppEvents;
using AD.ToolsCollection;

namespace AD.Game.Input
{
    public abstract class InputModule : IInputModule, IAppTickHandler, IAppLateTickHandler
    {
        private readonly ReactiveEvent onEnabled = new();
        private readonly ReactiveEvent onDisabled = new();

        public bool Enabled { get; private set; }
        public ReadOnlyReactiveEvent OnEnabled => onEnabled;
        public ReadOnlyReactiveEvent OnDisabled => onDisabled;

        public virtual void Enable()
        {
            Enabled = true;
            onEnabled.OnNext();
        }

        public virtual void Disable()
        {
            Enabled = false;
            onDisabled.OnNext();
        }

        void IAppTickHandler.OnTick(float deltaTime)
        {
            Update();
        }

        void IAppLateTickHandler.OnLateTick(float deltaTime)
        {
            LateUpdate();
        }

        protected virtual void Update()
        {
        }

        protected virtual void LateUpdate()
        {
        }
    }
}