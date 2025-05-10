using System.Linq;
using AD.ToolsCollection;

namespace Game.Instances
{
    [CreateWizard(typeof(InstanceRewardData))]
    public class InstanceRewardImportWizard : EntitiesImportWizard<int>
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "boss-loot";
        public override string SheetRange => "A:I";

        public override string IdKey => "ID";
        public override string TitleKey => "Boss";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var unitId = row["Boss ID"].IntParse();
            var mechanicId = row["Item Type ID"].IntParse();

            var mechanicParamsData = new[] { row["Param 1"], row["Param 2"] };
            var mechanicParams = mechanicParamsData
                .Where(x => x.IsValid())
                .ToList();

            data.GetProperty("unitId").SetValue(unitId);
            data.GetProperty("mechanicId").SetValue(mechanicId);
            data.GetProperty("mechanicParams").SetValue(mechanicParams);
        }
    }
}