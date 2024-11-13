using AD.Services.Router;

namespace Game.Inventory
{
    public class CharacterParamsVM : ViewModel
    {
        public int Power { get; }
        public int Health { get; }
        public int Resource { get; }

        public CharacterParamsVM(CharacterParams data)
        {
            Power = data.Power;
            Health = data.Health;
            Resource = data.Resource;
        }

        protected override void InitSubscribes()
        {
        }
    }
}