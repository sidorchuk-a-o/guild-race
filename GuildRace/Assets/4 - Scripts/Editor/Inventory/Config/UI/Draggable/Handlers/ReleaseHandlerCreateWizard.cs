using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [CreateWizard(typeof(ReleaseHandler))]
    public class ReleaseHandlerCreateWizard : CreateWizard
    {
        protected override void CreateWizardGUI(VisualElement root)
        {
            var typesCollection = InventoryEditorState
                .ReleaseHandlerFactory
                .EntityCollection;

            SetTypesCollection("Release Type", typesCollection);

            base.CreateWizardGUI(root);
        }
    }
}