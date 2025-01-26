using AD.ToolsCollection;
using UnityEditor;

namespace Game.Quests
{
    [CustomPropertyDrawer(typeof(QuestsGroup))]
    public class QuestsGroupProperty : KeyPropertyDrawer<QuestsGroup, int>
    {
    }
}