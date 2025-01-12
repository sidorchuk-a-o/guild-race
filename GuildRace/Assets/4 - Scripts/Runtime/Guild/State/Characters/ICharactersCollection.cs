using AD.States;

namespace Game.Guild
{
    public interface ICharactersCollection : IReadOnlyReactiveCollectionInfo<CharacterInfo>
    {
        CharacterInfo this[string id] { get; }
    }
}