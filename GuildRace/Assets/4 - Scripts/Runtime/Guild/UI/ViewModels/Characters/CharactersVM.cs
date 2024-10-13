using AD.Services.Router;

namespace Game.Guild
{
    public class CharactersVM : VMCollection<CharacterInfo, CharacterVM>
    {
        private readonly GuildVMFactory guildVMF;

        public CharactersVM(ICharactersCollection values, GuildVMFactory guildVMF) : base(values)
        {
            this.guildVMF = guildVMF;
        }

        protected override CharacterVM Create(CharacterInfo value)
        {
            return new CharacterVM(value, guildVMF);
        }
    }
}