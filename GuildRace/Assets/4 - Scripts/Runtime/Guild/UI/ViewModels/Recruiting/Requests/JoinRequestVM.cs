using AD.Services.Router;
using AD.ToolsCollection;
using Game.Items;

namespace Game.Guild
{
    public class JoinRequestVM : VMBase
    {
        public string Id { get; }
        public bool IsDefault { get; }

        public CharacterVM CharacterVM { get; }

        public JoinRequestVM(JoinRequestInfo info, GuildVMFactory guildVMF, ItemsVMFactory itemsVMF)
        {
            Id = info.Id;
            IsDefault = info.IsDefault;
            CharacterVM = new CharacterVM(info.Character, guildVMF, itemsVMF);
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            CharacterVM.AddTo(disp);
        }
    }
}