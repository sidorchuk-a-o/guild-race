using AD.Services.Router;
using AD.ToolsCollection;
using Game.Guild;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Instances
{
    public class SquadRolesCountersVM : ViewModel
    {
        private readonly ActiveInstanceInfo activeInstance;
        private readonly GuildVMFactory guildVMF;

        private readonly Dictionary<RoleId, SquadRolesCounterVM> counters;

        public IEnumerable<SquadRolesCounterVM> Counters => counters.Values;

        public SquadRolesCountersVM(
            SquadData squadData,
            ActiveInstanceInfo activeInstance,
            GuildVMFactory guildVMF)
        {
            this.activeInstance = activeInstance;
            this.guildVMF = guildVMF;

            counters = squadData.Roles
                .Select(x => new SquadRolesCounterVM(x, guildVMF))
                .ToDictionary(x => x.RoleVM.Id, x => x);
        }

        protected override void InitSubscribes()
        {
            counters.ForEach(x => x.Value.AddTo(this));

            activeInstance.Squad
                .ObserveAdd()
                .Subscribe(x => AddCharacterCallback(x.Value))
                .AddTo(this);

            activeInstance.Squad
                .ObserveRemove()
                .Subscribe(x => RemoveCharacterCallback(x.Value))
                .AddTo(this);
        }

        private void AddCharacterCallback(string characterId)
        {
            var character = guildVMF.GetCharacter(characterId);

            character.AddTo(this);

            var characterRole = character.SpecVM.Value.RoleVM;
            var counter = counters[characterRole.Id];

            counter.IncreaseCount();
        }

        private void RemoveCharacterCallback(string characterId)
        {
            var character = guildVMF.GetCharacter(characterId);

            character.AddTo(this);

            var characterRole = character.SpecVM.Value.RoleVM;
            var counter = counters[characterRole.Id];

            counter.DecreaseCount();
        }
    }
}