using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="ItemSlotsVMFactory"/>
    /// </summary>
    public abstract class ItemSlotsVMFactoryEditor : Editor
    {
        protected override void CreateTabItems(TabsContainer tabs)
        {
            tabs.CreateTab(CreateFactoryTab);
        }

        protected virtual void CreateFactoryTab(VisualElement root, SerializedData data)
        {
        }
    }
}