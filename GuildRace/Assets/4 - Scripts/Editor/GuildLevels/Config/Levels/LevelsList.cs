using AD.ToolsCollection;

namespace Game.GuildLevels
{
    public class LevelsList : ListElement<LevelData, LevelItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(LevelCreateWizard);

            base.BindData(data);
        }
    }
}