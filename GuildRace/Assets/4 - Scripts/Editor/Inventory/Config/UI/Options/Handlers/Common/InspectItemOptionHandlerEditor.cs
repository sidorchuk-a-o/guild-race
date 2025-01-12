using AD.ToolsCollection;

namespace Game.Inventory
{
    [Menu("Inspect", group: "Common")]
    [OptionHandlerEditor(typeof(InspectItemOptionHandler))]
    public class InspectItemOptionHandlerEditor : OpenWindowOptionHandlerEditor
    {
    }
}