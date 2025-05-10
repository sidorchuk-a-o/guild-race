using System;
using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace Game.Instances
{
    [CreateWizard(typeof(ConsumableMechanicHandler))]
    public class ConsumableMechanicHandlerImportWizard : EntitiesImportWizard<int>
    {
        private List<Type> subclasses;

        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "consume-data";
        public override string SheetRange => "A2:C";

        protected override Type GetDataType(IDataRow row)
        {
            subclasses ??= DataType.GetSubclasses().ToList();

            var typeName = row["Type"];
            var dataType = subclasses.FirstOrDefault(x => x.Name.Contains(typeName));

            return dataType;
        }
    }
}