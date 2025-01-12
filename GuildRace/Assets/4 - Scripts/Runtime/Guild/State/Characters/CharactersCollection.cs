using AD.States;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    public class CharactersCollection : ReactiveCollectionInfo<CharacterInfo>, ICharactersCollection
    {
        public CharacterInfo this[string id] => Values.FirstOrDefault(x => x.Id == id);

        public CharactersCollection(IEnumerable<CharacterInfo> values) : base(values)
        {
        }
    }
}