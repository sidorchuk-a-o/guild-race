using AD.Services.Router;

namespace Game.Instances
{
    public class IdsVM : VMCollection<string, IdVM>
    {
        public IdsVM(IIdsCollection values) : base(values)
        {
        }

        protected override IdVM Create(string value)
        {
            return new IdVM(value);
        }
    }
}