using AD.ToolsCollection;
using System.Collections.Generic;

namespace Game.Craft
{
    [CreateWizard(typeof(VendorData))]
    public class VendorImportWizard : EntitiesImportWizard<int>
    {
        public override string IdKey => "Id";
        public override string TitleKey => "Id";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "craft";
        public override string SheetRange => "A:H";

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);


        }
    }
}