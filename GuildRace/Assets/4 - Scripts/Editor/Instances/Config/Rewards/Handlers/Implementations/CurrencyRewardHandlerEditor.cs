using System.Collections.Generic;
using System.Threading.Tasks;
using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(CurrencyRewardHandler))]
    public class CurrencyRewardHandlerEditor : RewardHandlerEditor
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

            var currencyKey = new CurrencyKey(rewardParams[0]);
            var currencyValue = rewardParams[1];

            var currencyKeyElement = root.CreateKey<CurrencyKey, string>() as CurrencyKeyElement;
            var currencyLabel = root.CreateElement<LabelElement>();

            currencyKeyElement.ReadOnly();
            currencyKeyElement.filterOn = false;
            currencyKeyElement.removeOn = false;
            currencyKeyElement.updateOn = false;

            currencyKeyElement.value = currencyKey;
            currencyLabel.text = currencyValue;
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