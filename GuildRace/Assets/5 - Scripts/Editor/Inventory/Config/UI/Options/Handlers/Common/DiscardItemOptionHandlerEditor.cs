using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    [Menu("Discard", group: "Common")]
    [DataEditor(typeof(DiscardItemOptionHandler))]
    public class DiscardItemOptionHandlerEditor : OptionHandlerEditor
    {
    }
}