using AD.ToolsCollection;

namespace Game.Inventory
{
    [CreateWizard(typeof(ItemsGridCategoryData))]
    public class ItemsGridCategoryImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "Id";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "inventory-data";
        public override string SheetRange => "U2:W";
    }
}