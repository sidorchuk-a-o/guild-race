using System;
using System.Linq;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class OptionHandlerCreateWizard : CreateWizard
    {
        protected override Collection<Type> CreateTypesCollection()
        {
            var existHandlerTypes = InventoryEditorState.GetExistOptionHandlerTypes();
            var typesCollection = DataEditorsFactory.Instance.GetEntityCollection<OptionHandler>();

            typesCollection.SetFilter(x => !existHandlerTypes.Contains(x.Value));

            return typesCollection;
        }

        protected override void PreSave()
        {
            base.PreSave();

            if (newData is IEntity entity)
            {
                var factory = DataEditorsFactory.Instance;
                var editorData = factory.GetEditorData(entity.GetType());

                entity.SetTitle(editorData.MenuKey);
            }
        }
    }
}