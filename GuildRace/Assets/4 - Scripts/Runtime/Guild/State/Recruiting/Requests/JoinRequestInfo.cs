namespace Game.Guild
{
    public class JoinRequestInfo
    {
        public string Id { get; }

        public bool IsDefault { get; }
        public long CreatedTime { get; }

        public CharacterInfo Character { get; }

        public JoinRequestInfo(CharacterInfo character, long createdTime)
        {
            Id = character.Id;
            Character = character;
            CreatedTime = createdTime;
            IsDefault = CreatedTime == -1;
        }
    }
}