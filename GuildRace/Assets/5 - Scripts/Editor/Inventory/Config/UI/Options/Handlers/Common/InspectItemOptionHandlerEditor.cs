using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    [Menu("Inspect", group: "Common")]
    [DataEditor(typeof(InspectItemOptionHandler))]
    public class InspectItemOptionHandlerEditor : OpenWindowOptionHandlerEditor
    {
    }
}