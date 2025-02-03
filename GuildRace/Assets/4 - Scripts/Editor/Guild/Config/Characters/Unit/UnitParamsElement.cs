using AD.ToolsCollection;

namespace Game.Guild
{
    /// <summary>
    /// Element: <see cref="UnitParams"/>
    /// </summary>
    public class UnitParamsElement : Element
    {
        private PropertyElement healthField;
        private PropertyElement powerField;
        private ResourceParamsElement resourceParamsField;

        protected override void CreateElementGUI(Element root)
        {
            healthField = root.CreateProperty();
            powerField = root.CreateProperty();

            root.CreateSpace();

            resourceParamsField = root.CreateElement<ResourceParamsElement>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            healthField.BindProperty("health", data);
            powerField.BindProperty("power", data);
            resourceParamsField.BindProperty("resourceParams", data);
        }
    }
}