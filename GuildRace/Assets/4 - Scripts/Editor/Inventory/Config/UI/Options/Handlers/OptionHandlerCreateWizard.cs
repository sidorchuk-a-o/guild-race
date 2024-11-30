using AD.ToolsCollection;
using System.Linq;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    [CreateWizard(typeof(OptionHandler))]
    public class OptionHandlerCreateWizard : CreateWizard
    {
        protected override void CreateWizardGUI(VisualElement root)
        {
            var existHandlerTypes = InventoryEditorState.GetExistOptionHandlerTypes();
            var typesCollection = InventoryEditorState.OptionHandlerFactory.EntityCollection;

            typesCollection.SetFilter(x => !existHandlerTypes.Contains(x.value));

            SetTypesCollection("Handler", typesCollection);

            base.CreateWizardGUI(root);
        }

        protected override void PreSave()
        {
            base.PreSave();

            if (newData is IEntity entity)
            {
                var factory = InventoryEditorState.OptionHandlerFactory;
                var editorData = factory.GetEditorData(entity.GetType());

                entity.SetTitle(editorData.MenuKey);
            }
        }
    }
}