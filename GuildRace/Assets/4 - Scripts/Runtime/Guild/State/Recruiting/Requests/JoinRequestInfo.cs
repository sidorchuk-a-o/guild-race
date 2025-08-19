using System;

namespace Game.Guild
{
    public class JoinRequestInfo : IEquatable<JoinRequestInfo>
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

        // == IEquatable ==

        public bool Equals(JoinRequestInfo other)
        {
            return other is not null
                && other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as JoinRequestInfo);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}