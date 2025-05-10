namespace Game.Instances
{
    public class SquadChanceParams
    {
        public ActiveInstanceInfo SetupInstance { get; }

        public float Health { get; set; }
        public float Power { get; set; }

        public float ConsumableChance { get; set; }

        public SquadChanceParams(ActiveInstanceInfo setupInstance)
        {
            SetupInstance = setupInstance;
        }
    }
}