using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class CharactersCollection : ReactiveCollection<CharacterInfo>, ICharactersCollection
    {
        public CharacterInfo this[string id] => Values.FirstOrDefault(x => x.Id == id);

        public CharactersCollection(IEnumerable<CharacterInfo> values) : base(values)
        {
        }
    }
}