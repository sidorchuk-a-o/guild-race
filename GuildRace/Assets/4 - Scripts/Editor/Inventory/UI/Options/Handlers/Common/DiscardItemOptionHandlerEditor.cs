using AD.ToolsCollection;

namespace Game.Inventory
{
    [Menu("Discard", group: "Common")]
    [OptionHandlerEditor(typeof(DiscardItemOptionHandler))]
    public class DiscardItemOptionHandlerEditor : OptionHandlerEditor
    {
    }
}