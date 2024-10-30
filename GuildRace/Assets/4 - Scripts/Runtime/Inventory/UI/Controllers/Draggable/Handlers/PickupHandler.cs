using AD.ToolsCollection;

namespace Game.Inventory
{
    public abstract class PickupHandler : ScriptableData
    {
        public void StartProcess(PickupResult result)
        {
            if (CheckContext(result))
            {
                Process(result);
            }
        }

        protected abstract bool CheckContext(PickupResult result);
        protected abstract void Process(PickupResult result);
    }
}