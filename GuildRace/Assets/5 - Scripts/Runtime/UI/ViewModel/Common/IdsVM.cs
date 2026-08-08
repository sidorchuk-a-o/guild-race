using AD.Router;

namespace AD.Game.Instances
{
    public class IdsVM : VMReactiveCollection<string, IdVM>
    {
        public IdsVM(IIdsCollection values) : base(values)
        {
        }

        protected override IdVM Create(string value)
        {
            return new(value);
        }
    }
}