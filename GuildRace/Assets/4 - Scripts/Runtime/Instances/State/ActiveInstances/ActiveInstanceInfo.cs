using Game.Inventory;
using System;
using System.Collections.Generic;
using UniRx;

namespace Game.Instances
{
    public class ActiveInstanceInfo : IEquatable<ActiveInstanceInfo>
    {
        private readonly IdsCollection squad;
        private readonly ReactiveProperty<bool> isReadyToComplete = new();

        public string Id { get; }
        public InstanceInfo Instance { get; }

        public ItemsGridInfo Bag { get; }
        public IIdsCollection Squad => squad;

        public long StartTime { get; private set; }
        public IReadOnlyReactiveProperty<bool> IsReadyToComplete => isReadyToComplete;

        public ActiveInstanceInfo(
            string id,
            InstanceInfo inst,
            ItemsGridInfo bag,
            IEnumerable<string> squad)
        {
            Id = id;
            Instance = inst;
            Bag = bag;

            this.squad = new(squad);
        }

        public void AddCharacter(string characterId)
        {
            squad.Add(characterId);
        }

        public void RemoveCharacter(string characterId)
        {
            squad.Remove(characterId);
        }

        public void SetStartTime(long value)
        {
            StartTime = value;
        }

        public void MarAsReadyToComplete()
        {
            isReadyToComplete.Value = true;
        }

        // == IEquatable ==

        public bool Equals(ActiveInstanceInfo other)
        {
            return other != null
                && other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ActiveInstanceInfo);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}