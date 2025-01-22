using AD.ToolsCollection;

namespace Game.Craft
{
    /// <summary>
    /// Element: <see cref="RecycleSlotData"/>
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
                var newData = DataFactory.Create<RecycleSlotData>();
                var saveMeta = new SaveMeta(isSubObject: true, data);

                newData.SetTitle("Recycle Slot");

                DataFactory.Save(newData, saveMeta);

                data.SetValue(newData);
            }
        }
    }
}