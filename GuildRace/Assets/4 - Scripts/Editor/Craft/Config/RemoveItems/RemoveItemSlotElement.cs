using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Element: <see cref="RemoveItemSlotData"/>
    /// </summary>
    public class RemoveItemSlotElement : Element
    {
        private Label labelField;

        protected override void CreateElementGUI(Element root)
        {
            labelField = root.Create<Label>();
            labelField.Height(50).FontSize(16).TextAnchor(UnityEngine.TextAnchor.MiddleCenter);
            labelField.text = "RemoveItemSlot - Created";
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            if (data.GetValue() == null)
            {
                var newData = DataFactory.Create<RemoveItemSlotData>();
                var saveMeta = new SaveMeta(isSubObject: true, data);

                DataFactory.Save(newData, saveMeta);

                data.SetValue(newData);
            }
        }
    }
}