using AD.States;
using System.Collections.Generic;

namespace Game.Guild
{
    public class CharactersCollection : ReactiveCollectionInfo<CharacterInfo>, ICharactersCollection
    {
        public CharactersCollection(IEnumerable<CharacterInfo> values) : base(values)
        {
        }
    }
}