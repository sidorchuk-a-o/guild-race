using AD.Services.Router;

namespace Game.Guild
{
    public class JoinRequestVM : ViewModel
    {
        public string Id { get; }
        public bool IsDefault { get; }

        public CharacterVM CharacterVM { get; }

        public JoinRequestVM(JoinRequestInfo info, GuildVMFactory guildVMF)
        {
            Id = info.Id;
            IsDefault = info.IsDefault;
            CharacterVM = new CharacterVM(info.Character, guildVMF);
        }

        protected override void InitSubscribes()
        {
            CharacterVM.AddTo(this);
        }
    }
}