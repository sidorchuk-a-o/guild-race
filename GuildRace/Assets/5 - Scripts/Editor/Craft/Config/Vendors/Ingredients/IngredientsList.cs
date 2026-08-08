using System;
using AD.ToolsCollection;

namespace AD.Game.Craft
{
    public class IngredientsList : ListElement<IngredientData>
    {
        protected override Type ItemType { get; } = typeof(IngredientItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}