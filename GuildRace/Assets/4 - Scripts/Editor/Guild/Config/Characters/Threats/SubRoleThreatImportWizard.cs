using AD.ToolsCollection;
using Game.Instances;

namespace Game.Guild
{
    [CreateWizard(typeof(SubRoleThreatData))]
    public class SubRoleThreatImportWizard : GoogleSheetsImportWizard
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "class-data";
        public override string SheetRange => "R2:T";

        public override string ValidKey => "Subrole ID";

        protected override bool CheckEqual(SerializedData data, IDataRow row)
        {
            var subRoleId = data.GetProperty("subRoleId").GetValue<SubRoleId>();
            var rowData = row["Subrole ID"].IntParse();

            return subRoleId == rowData;
        }

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            var subRoleId = new SubRoleId(row["Subrole ID"].IntParse());
            var threatId = new ThreatId(row["Threat ID"].IntParse());

            data.GetProperty("subRoleId").SetValue(subRoleId);
            data.GetProperty("threatId").SetValue(threatId);
        }
    }
}