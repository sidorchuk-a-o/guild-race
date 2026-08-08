using System;
using AD.ToolsCollection;

namespace AD.Game.GuildLevels
{
    public class LevelsList : ListElement<LevelData>
    {
        protected override Type ItemType { get; } = typeof(LevelItem);
    }
}