using System.Collections.Generic;
using System.Threading.Tasks;
using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(EquipRewardHandler))]
    public class EquipRewardHandlerEditor : RewardHandlerEditor
    {
        private EquipRewardParamsList parametersList;

        protected override void CreateContentGUI(VisualElement root)
        {
            base.CreateContentGUI(root);

            parametersList = root.CreateElement<EquipRewardParamsList>();
            parametersList.FlexWidth(33).MarginRight(10);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            parametersList.BindProperty("parameters", data);
        }

        // == Params Editor ==

        public override void CreateParamsEditor(VisualElement root, List<string> rewardParams)
        {
            root.ConvertToRow();

            var equipId = rewardParams[0].IntParse();
            var countValue = rewardParams[1];

            var countLabel = root.CreateElement<LabelElement>();
            var equipElement = root.CreatePopup(InventoryEditorState.GetAllItemsCollection());

            countLabel.Text = countValue;
            equipElement.Value = equipId;

            equipElement.ReadOnly();
        }

        // == Parse Params ==

        public override async Task ParseAndApplyParams(SerializedData data, string sheetParams, string sheetId)
        {
            var saveMeta = new SaveMeta(isSubObject: true, data.GetProperty("parameters"));
            var importer = sheetParams.ParseImporter(sheetId, typeof(EquipRewardParams));

            await importer.LoadData("Type ID");

            importer.ImportData(saveMeta, CheckEqual, UpdateData);
        }

        private static bool CheckEqual(SerializedData data, IDataRow row)
        {
            var instanceType = data.GetProperty("instanceType").GetValue<InstanceType>();
            var typeId = row["Type ID"].IntParse();

            return typeId == instanceType;
        }

        private static void UpdateData(SerializedData data, IDataRow row)
        {
            var instanceType = new InstanceType(row["Type ID"].IntParse());
            var guaranteedCount = row["Guaranteed Count"].IntParse();
            var chanceCount = row["Chance Count"].IntParse();
            var chance = row["Chance"].FloatParse();

            data.GetProperty("instanceType").SetValue(instanceType);
            data.GetProperty("guaranteedCount").SetValue(guaranteedCount);
            data.GetProperty("chanceCount").SetValue(chanceCount);
            data.GetProperty("chance").SetValue(chance);
        }
    }
}