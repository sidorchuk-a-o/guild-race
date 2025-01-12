using AD.Services.Router;

namespace Game.Instances
{
    public class IdVM : ViewModel
    {
        public string Value { get; }

        public IdVM(string value)
        {
            Value = value;
        }

        protected override void InitSubscribes()
        {
        }
    }
}