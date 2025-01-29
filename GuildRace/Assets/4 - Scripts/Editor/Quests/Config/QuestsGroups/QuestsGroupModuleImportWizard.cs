using AD.Services.Localization;
using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Quests
{
    [CreateWizard(typeof(QuestsGroupModule))]
    public class QuestsGroupModuleImportWizard : EntitiesImportWizard<int>
    {
        private List<Type> subclasses;

        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "quest-data";
        public override string SheetRange => "A2:E";

        public override string IdKey => "ID";
        public override string TitleKey => "Name";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var nameKey = row["Name Key"].LocalizeKeyParse();
            var maxQuestsCount = row["Max Count"].IntParse();

            data.GetProperty("nameKey").SetValue(nameKey);
            data.GetProperty("maxQuestsCount").SetValue(maxQuestsCount);
        }

        protected override Type GetDataType(IDataRow row)
        {
            subclasses ??= DataType.GetSubclasses().ToList();

            var typeName = row["Type"];
            var dataType = subclasses.FirstOrDefault(x => x.Name.Contains(typeName));

            return dataType;
        }
    }
}