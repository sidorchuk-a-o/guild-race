using System.Collections.Generic;
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
    }
}