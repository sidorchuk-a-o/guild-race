using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public interface ICharactersCollection : IReadOnlyReactiveCollection<CharacterInfo>
    {
        CharacterInfo this[string id] { get; }
    }
}