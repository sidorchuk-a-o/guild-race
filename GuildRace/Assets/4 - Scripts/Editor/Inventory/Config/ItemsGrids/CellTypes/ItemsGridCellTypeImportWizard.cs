using AD.ToolsCollection;

namespace Game.Inventory
{
    [CreateWizard(typeof(ItemsGridCellTypeData))]
    public class ItemsGridCellTypeImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "inventory-data";
        public override string SheetRange => "X2:Y";
    }
}