using AD.ToolsCollection;

namespace Game.Instances
{
    [CreateWizard(typeof(ConsumableTypeData))]
    public class ConsumableTypeImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "consume-data";
        public override string SheetRange => "E2:F";
    }
}