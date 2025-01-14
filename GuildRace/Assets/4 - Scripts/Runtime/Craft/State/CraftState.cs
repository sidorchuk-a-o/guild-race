using AD.Services;
using AD.Services.Save;
using VContainer;

namespace Game.Craft
{
    public class CraftState : ServiceState<CraftConfig, CraftStateSM>
    {
        public override string SaveKey => CraftStateSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public CraftState(CraftConfig config, IObjectResolver resolver) : base(config, resolver)
        {
        }

        // == Save ==

        protected override CraftStateSM CreateSave()
        {
            return new CraftStateSM();
        }

        protected override void ReadSave(CraftStateSM save)
        {
        }
    }
}