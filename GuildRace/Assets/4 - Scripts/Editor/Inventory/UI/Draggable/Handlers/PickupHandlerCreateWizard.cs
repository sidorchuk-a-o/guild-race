using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [CreateWizard(typeof(PickupHandler))]
    public class PickupHandlerCreateWizard : CreateWizard
    {
        protected override void CreateWizardGUI(VisualElement root)
        {
            var typesCollection = InventoryEditorState
                .PickupHandlersFactory
                .EntityCollection;

            SetTypesCollection("Handler Type", typesCollection);

            base.CreateWizardGUI(root);
        }
    }
}