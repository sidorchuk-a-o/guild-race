using AD.Services.Router;

namespace Game.Inventory
{
    public class CharacterParamsVM : ViewModel
    {
        public float Power { get; }
        public float Health { get; }
        public float Armor { get; }
        public float Resource { get; }

        public CharacterParamsVM(CharacterParams data)
        {
            Power = data.Power;
            Armor = data.Armor;
            Health = data.Health;
            Resource = data.Resource;
        }

        protected override void InitSubscribes()
        {
        }
    }
}