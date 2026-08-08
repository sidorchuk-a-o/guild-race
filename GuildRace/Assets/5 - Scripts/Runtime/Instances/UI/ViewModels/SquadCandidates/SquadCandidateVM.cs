using AD.Game.Guild;
using AD.Router;

namespace AD.Game.Instances
{
    public class SquadCandidateVM : ViewModel
    {
        public CharacterVM CharacterVM { get; }
        public ThreatsVM ThreatsVM { get; }

        public SquadCandidateVM(SquadCandidateInfo info, InstancesVMFactory instancesVMF)
        {
            CharacterVM = instancesVMF.GuildVMF.GetCharacter(info.CharacterId);
            ThreatsVM = new(info.Threads, instancesVMF);
        }

        protected override void InitSubscribes()
        {
            CharacterVM.AddTo(this);
            ThreatsVM.AddTo(this);
        }
    }
}