using AD.Services.Localization;
using AD.ToolsCollection;
using AD.UI;

namespace Game.GuildLevels
{
    public abstract class LevelMechanic : ScriptableData
    {
        public abstract void Apply(LevelContext context);

        public virtual UITextData GetDesc(LocalizeKey descKey)
        {
            return new UITextData(descKey);
        }
    }
}