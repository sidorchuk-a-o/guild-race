using AD.Services.Router;

namespace Game.Guild
{
    public class JoinRequestsVM : VMCollection<JoinRequestInfo, JoinRequestVM>
    {
        private readonly GuildVMFactory guildVMF;

        public JoinRequestsVM(IJoinRequestsCollection values, GuildVMFactory guildVMF) : base(values)
        {
            this.guildVMF = guildVMF;
        }

        protected override JoinRequestVM Create(JoinRequestInfo value)
        {
            return new JoinRequestVM(value, guildVMF);
        }
    }
}