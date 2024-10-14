using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Guild
{
    public class ClassWeightVM : VMBase
    {
        private readonly ClassWeightInfo info;
        private readonly GuildVMFactory guildVMF;

        public ClassVM ClassVM { get; }

        public ClassWeightVM(ClassWeightInfo info, GuildVMFactory guildVMF)
        {
            this.info = info;
            this.guildVMF = guildVMF;

            ClassVM = guildVMF.GetClass(info.ClassId);

            SetSelectState(info.IsEnabled.Value);
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            ClassVM.AddTo(disp);

            IsSelected
                .SilentSubscribe(SelectStateChanged)
                .AddTo(disp);
        }

        private void SelectStateChanged(bool isSelected)
        {
            guildVMF.SetClassWeightState(info.ClassId, isSelected);
        }
    }
}