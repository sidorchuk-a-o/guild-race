using AD.Services.Router;
using Game.Inventory;

namespace Game.Guild
{
    public class JoinRequestsVM : VMCollection<JoinRequestInfo, JoinRequestVM>
    {
        private readonly GuildVMFactory guildVMF;
        private readonly InventoryVMFactory inventoryVMF;

        public JoinRequestsVM(
            IJoinRequestsCollection values,
            GuildVMFactory guildVMF,
            InventoryVMFactory inventoryVMF)
            : base(values)
        {
            this.guildVMF = guildVMF;
            this.inventoryVMF = inventoryVMF;
        }

        protected override JoinRequestVM Create(JoinRequestInfo value)
        {
            return new JoinRequestVM(value, guildVMF, inventoryVMF);
        }
    }
}