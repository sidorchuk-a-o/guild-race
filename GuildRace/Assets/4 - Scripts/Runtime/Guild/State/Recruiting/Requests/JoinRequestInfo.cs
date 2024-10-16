using System;

namespace Game.Guild
{
    public class JoinRequestInfo
    {
        public string Id { get; }

        public bool IsDefault { get; }
        public DateTime CreatedTime { get; }

        public CharacterInfo Character { get; }

        public JoinRequestInfo(CharacterInfo character, DateTime createdTime)
        {
            Id = character.Id;
            Character = character;
            CreatedTime = createdTime;
            IsDefault = CreatedTime == DateTime.MinValue;
        }
    }
}