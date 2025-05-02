using AD.Services.Store;
using AD.ToolsCollection;
using System.Linq;
using UnityEngine;

namespace Game.Quests
{
    [CreateWizard(typeof(QuestData))]
    public class QuestImportWizard : EntitiesImportWizard<int>
    {
        public override string SheetId => "185chfmtv9Q6kwfZp5aEcVKDK0s9oAtXJbDfOPk1Nkd0";
        public override string SheetName => "quests";
        public override string SheetRange => "A:M";

        public override string IdKey => "ID";
        public override string TitleKey => "Desc";

        protected override void UpdateData(SerializedData data, IDataRow row)
        {
            base.UpdateData(data, row);

            var groupId = new QuestsGroup(row["Group ID"].IntParse());
            var mechanicId = row["Mech ID"].IntParse();
            var requiredProgress = Mathf.Max(row["Progress"].IntParse(), 1);

            var mechanicParamsData = new[] { row["Param 1"], row["Param 2"] };
            var mechanicParams = mechanicParamsData
                .Where(x => x.IsValid())
                .ToList();

            var rewardId = new CurrencyKey(row["Reward ID"]);
            var rewardAmount = row["Reward Amount"].LongParse();
            var reward = new CurrencyAmount(rewardId, rewardAmount);

            data.GetProperty("groupId").SetValue(groupId);
            data.GetProperty("mechanicId").SetValue(mechanicId);
            data.GetProperty("requiredProgress").SetValue(requiredProgress);
            data.GetProperty("mechanicParams").SetValue(mechanicParams);
            data.GetProperty("reward").SetValue((CurrencyAmountData)reward);
        }
    }
}