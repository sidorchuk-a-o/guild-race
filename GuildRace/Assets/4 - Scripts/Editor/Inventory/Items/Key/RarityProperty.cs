using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [CustomPropertyDrawer(typeof(Rarity))]
    public class RarityProperty : KeyPropertyDrawer<Rarity>
    {
    }
}