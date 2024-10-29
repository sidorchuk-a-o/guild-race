using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Editor: <see cref="ItemsVMFactory"/>
    /// </summary>
    public abstract class ItemsVMFactoryEditor : Editor
    {
        private AddressableElement<GameObject> itemInGridRefField;
        private AddressableElement<GameObject> itemInSlotRefField;

        protected override void CreateTabItems(TabsContainer tabs)
        {
            tabs.CreateTab(CreateFactoryTab);
        }

        protected virtual void CreateFactoryTab(VisualElement root, SerializedData data)
        {
            itemInGridRefField = root.CreateAddressable<GameObject>();
            itemInGridRefField.BindProperty("itemInGridRef", data);

            itemInSlotRefField = root.CreateAddressable<GameObject>();
            itemInSlotRefField.BindProperty("itemInSlotRef", data);
        }
    }
}