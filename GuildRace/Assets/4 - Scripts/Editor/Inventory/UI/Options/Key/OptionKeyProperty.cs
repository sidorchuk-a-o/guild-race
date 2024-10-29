using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [CustomPropertyDrawer(typeof(OptionKey))]
    public class OptionKeyProperty : KeyPropertyDrawer<OptionKey>
    {
    }
}