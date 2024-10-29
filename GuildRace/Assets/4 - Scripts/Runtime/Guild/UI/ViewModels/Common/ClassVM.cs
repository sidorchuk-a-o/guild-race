using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;

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

        protected override void InitSubscribes(CompositeDisp disp)
        {
        }
    }
}