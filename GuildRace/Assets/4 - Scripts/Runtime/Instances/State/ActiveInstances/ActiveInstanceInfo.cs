using Game.Inventory;
using System.Collections.Generic;

namespace Game.Instances
{
    public class ActiveInstanceInfo
    {
        private readonly IdsCollection squad;

        public string Id { get; }

        public int InstanceId { get; }
        public IIdsCollection Squad => squad;
        public ItemsGridInfo Bag { get; }

        public ActiveInstanceInfo(
            string id,
            int instanceId,
            ItemsGridInfo bag,
            IEnumerable<string> squad)
        {
            Id = id;
            InstanceId = instanceId;
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
    }
}