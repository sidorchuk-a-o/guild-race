using AD.ToolsCollection;

namespace AD.Game.Craft
{
    /// <summary>
    ///     Element: <see cref="RecycleSlotData" />
    /// </summary>
    public class RecycleSlotElement : Element
    {
        protected override void CreateElementGUI(Element root)
        {
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            if (data.GetValue() == null)
            {
                var newData = DataFactory.CreateInstance<RecycleSlotData>();
                var saveMeta = new SaveMeta(isSubObject: true, data);

                newData.SetTitle("Recycle Slot");

                DataFactory.SaveInstance(newData, saveMeta);

                data.SetValue(newData);
            }
        }
    }
}