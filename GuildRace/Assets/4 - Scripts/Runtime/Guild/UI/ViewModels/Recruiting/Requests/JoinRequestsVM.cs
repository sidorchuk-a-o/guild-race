using AD.Services.Router;
using Game.Items;

namespace Game.Guild
{
    public class JoinRequestsVM : VMCollection<JoinRequestInfo, JoinRequestVM>
    {
        private readonly GuildVMFactory guildVMF;
        private readonly ItemsVMFactory itemsVMF;

        public JoinRequestsVM(
            IJoinRequestsCollection values,
            GuildVMFactory guildVMF,
            ItemsVMFactory itemsVMF)
            : base(values)
        {
            this.guildVMF = guildVMF;
            this.itemsVMF = itemsVMF;
        }

        protected override JoinRequestVM Create(JoinRequestInfo value)
        {
            return new JoinRequestVM(value, guildVMF, itemsVMF);
        }
    }
}