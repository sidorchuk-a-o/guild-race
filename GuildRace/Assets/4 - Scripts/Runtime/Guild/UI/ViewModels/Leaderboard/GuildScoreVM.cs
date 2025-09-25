using AD.Services.Router;

namespace Game.Guild
{
    public class GuildScoreVM : ViewModel
    {
        public string GuildName { get; }
        public EmblemVM EmblemVM { get; }

        public GuildScoreVM(GuildScoreData data, GuildVMFactory guildVMF)
        {
            GuildName = data.GuildName;
            EmblemVM = guildVMF.GetEmblem(data.Emblem);
        }

        protected override void InitSubscribes()
        {
            EmblemVM.AddTo(this);
        }
    }
}