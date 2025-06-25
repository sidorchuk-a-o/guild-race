using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="EquipRewardParams"/>
    /// </summary>
    public class EquipRewardParamsItem : ListItemElement
    {
        private PropertyElement instanceTypeField;
        private PropertyElement guaranteedCountField;
        private PropertyElement chanceCountField;
        private PropertyElement chanceField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            root.ConvertToColumn();

            base.CreateItemContentGUI(root);

            instanceTypeField = root.CreateProperty();
            guaranteedCountField = root.CreateProperty();
            chanceCountField = root.CreateProperty();
            chanceField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            instanceTypeField.BindProperty("instanceType", data);
            guaranteedCountField.BindProperty("guaranteedCount", data);
            chanceCountField.BindProperty("chanceCount", data);
            chanceField.BindProperty("chance", data);
        }
    }
}