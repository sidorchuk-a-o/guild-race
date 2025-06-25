using System.Collections.Generic;
using System.Threading.Tasks;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(ReagentRewardHandler))]
    public class ReagentRewardHandlerEditor : RewardHandlerEditor
    {
        private PropertyElement failedModField;

        protected override void CreateSimpleContentGUI(VisualElement root)
        {
            base.CreateSimpleContentGUI(root);

            failedModField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            failedModField.BindProperty("failedMod", data);
        }

        // == Params Editor ==

        public override void CreateParamsEditor(VisualElement root, List<string> rewardParams)
        {
            root.ConvertToRow();

            var equipId = rewardParams[0].IntParse();
            var countValue = rewardParams[1];

            var countLabel = root.CreateElement<LabelElement>();
            var equipElement = root.CreatePopup(InventoryEditorState.GetAllItemsCollection());

            countLabel.text = countValue;
            equipElement.value = equipId;

            equipElement.ReadOnly();
        }

        // == Parse Params ==

        public override async Task ParseAndApplyParams(SerializedData data, string sheetParams, string sheetId)
        {
            var importer = sheetParams.ParseImporter(sheetId);

            await importer.LoadData("Failed Mod");

            importer.ImportData((i, row) =>
            {
                var failedMod = row["Failed Mod"].FloatParse();

                data.GetProperty("failedMod").SetValue(failedMod);
            });
        }
    }
}