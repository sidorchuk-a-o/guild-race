using System.Collections.Generic;
using System.Threading.Tasks;
using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(ReagentRewardHandler))]
    public class ReagentRewardHandlerEditor : RewardHandlerEditor
    {
        private PropertyElement failedModField;

        protected override void CreateContentGUI(VisualElement root)
        {
            base.CreateContentGUI(root);

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

            countLabel.Text = countValue;
            equipElement.Value = equipId;

            equipElement.ReadOnly();
        }

        // == Parse Params ==

        public override async Task ParseAndApplyParams(SerializedData data, string sheetParams, string sheetId)
        {
            var importer = sheetParams.ParseImporter(sheetId);

            await importer.LoadData("Failed Mod");

            importer.ImportData((_, row) =>
            {
                var failedMod = row["Failed Mod"].FloatParse();

                data.GetProperty("failedMod").SetValue(failedMod);
            });
        }
    }
}