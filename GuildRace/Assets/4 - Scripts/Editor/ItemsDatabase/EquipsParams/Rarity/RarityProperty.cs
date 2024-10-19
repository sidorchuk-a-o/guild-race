using AD.ToolsCollection;
using UnityEditor;

namespace Game.Items
{
    [CustomPropertyDrawer(typeof(Rarity))]
    public class RarityProperty : KeyPropertyDrawer<Rarity>
    {
    }
}