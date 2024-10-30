using AD.Services.Localization;
using AD.Services.Router;

namespace Game.Guild
{
    public class ClassVM : ViewModel
    {
        public ClassId Id { get; }
        public LocalizeKey NameKey { get; }

        public ClassVM(ClassData data)
        {
            Id = data.Id;
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }
    }
}