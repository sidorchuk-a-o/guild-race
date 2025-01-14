using AD.ToolsCollection;
using Game.Inventory;
using System.Collections.Generic;

namespace Game.Craft
{
    [CreateWizard(typeof(ReagentItemData))]
    public class ReagentItemImportWizard : ItemDataImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "resources";
        public override string SheetRange => "A:E";

        public override string NameLocalizeKey => IdKey;

        protected override void UpdateData(SerializedData data, IReadOnlyDictionary<string, string> row)
        {
            base.UpdateData(data, row);


        }
    }
}