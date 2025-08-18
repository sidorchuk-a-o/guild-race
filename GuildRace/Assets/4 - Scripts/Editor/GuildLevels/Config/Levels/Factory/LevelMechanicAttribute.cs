using System;
using AD.ToolsCollection;

namespace Game.GuildLevels
{
    public class LevelMechanicAttribute : EditorAttribute
    {
        public LevelMechanicAttribute(Type dataType) : base(dataType)
        {
        }
    }
}