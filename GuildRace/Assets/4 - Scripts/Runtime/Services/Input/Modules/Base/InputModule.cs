using AD.Services.AppEvents;
using AD.ToolsCollection;

namespace Game.Input
{
    public abstract class InputModule : IInputModule, IAppTickListener
    {
        private readonly Subject onEnabled = new();
        private readonly Subject onDisabled = new();

        public bool Enabled { get; private set; }
        public IObservable OnEnabled => onEnabled;
        public IObservable OnDisabled => onDisabled;

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

        void IAppTickListener.OnTick(float deltaTime)
        {
            Update();
        }

        void IAppTickListener.OnLateTick(float deltaTime)
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