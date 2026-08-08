using System.Collections.Generic;
using System.Threading.Tasks;
using AD.Store;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(CurrencyRewardHandler))]
    public class CurrencyRewardHandlerEditor : RewardHandlerEditor
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

            var currencyKey = new CurrencyKey(rewardParams[0]);
            var currencyValue = rewardParams[1];

            var currencyLabel = root.CreateElement<LabelElement>();
            var currencyKeyElement = (CurrencyKeyElement)root.CreateKeyPopup<CurrencyKey, string>();

            currencyKeyElement.ReadOnly().FlexGrow(1);
            currencyKeyElement.FilterOn = false;
            currencyKeyElement.RemoveOn = false;
            currencyKeyElement.UpdateOn = false;

            currencyKeyElement.Value = currencyKey;
            currencyLabel.Text = currencyValue;
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