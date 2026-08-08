using AD.Router;

namespace AD.Game.Inventory
{
    public class CharacterParamsVM : ViewModel
    {
        public float Power { get; }
        public float Health { get; }

        public CharacterParamsVM(CharacterParams data)
        {
            Power = data.Power;
            Health = data.Health;
        }

        protected override void InitSubscribes()
        {
        }
    }
}