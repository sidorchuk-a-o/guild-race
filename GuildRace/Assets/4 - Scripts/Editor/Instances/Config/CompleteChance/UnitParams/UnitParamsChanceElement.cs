using AD.ToolsCollection;

namespace Game.Instances
{
    /// <summary>
    /// Element: <see cref="UnitParamsChanceData"/>
    /// </summary>
    public class UnitParamsChanceElement : Element
    {
        private PropertyElement totalModField;
        private PropertyElement charactersModField;

        protected override void CreateElementGUI(Element root)
        {
            totalModField = root.CreateProperty();
            charactersModField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            totalModField.BindProperty("totalMod", data);
            charactersModField.BindProperty("charactersMod", data);
        }
    }
}