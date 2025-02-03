using AD.ToolsCollection;

namespace Game.Guild
{
    /// <summary>
    /// Element: <see cref="ResourceParams"/>
    /// </summary>
    public class ResourceParamsElement : Element
    {
        private PropertyElement maxValueField;
        private PropertyElement regenValueField;

        protected override void CreateElementGUI(Element root)
        {
            maxValueField = root.CreateProperty();
            regenValueField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            maxValueField.BindProperty("maxValue", data);
            regenValueField.BindProperty("regenValue", data);

        }
    }
}