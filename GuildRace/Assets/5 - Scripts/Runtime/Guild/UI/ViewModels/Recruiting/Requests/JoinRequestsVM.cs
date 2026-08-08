using AD.Router;

namespace AD.Game.Guild
{
    public class JoinRequestsVM : VMReactiveCollection<JoinRequestInfo, JoinRequestVM>
    {
        private readonly GuildVMFactory guildVMF;

        public JoinRequestsVM(IJoinRequestsCollection values, GuildVMFactory guildVMF)
            : base(values)
        {
            this.guildVMF = guildVMF;
        }

        protected override JoinRequestVM Create(JoinRequestInfo value)
        {
            return new(value, guildVMF);
        }
    }
}