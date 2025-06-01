using System.Threading;
using Cysharp.Threading.Tasks;
using AD.Services.Localization;
using AD.Services.Router;
using UnityEngine;

namespace Game.Guild
{
    public class SpecializationVM : ViewModel
    {
        private readonly SpecializationData data;
        private readonly GuildVMFactory guildVMF;

        public SpecializationId Id { get; }
        public LocalizeKey NameKey { get; }

        public RoleVM RoleVM { get; }
        public SubRoleVM SubRoleVM { get; }
        public AbilitiesVM AbilitiesVM { get; }

        public SpecializationVM(SpecializationData data, GuildVMFactory guildVMF)
        {
            this.data = data;
            this.guildVMF = guildVMF;

            Id = data.Id;
            NameKey = data.NameKey;

            RoleVM = guildVMF.GetRole(data.RoleId);
            SubRoleVM = guildVMF.GetSubRole(data.SubRoleId);
            AbilitiesVM = new AbilitiesVM(data.Abilities, guildVMF.InstancesVMF);
        }

        protected override void InitSubscribes()
        {
            RoleVM.AddTo(this);
            SubRoleVM.AddTo(this);
            AbilitiesVM.AddTo(this);
        }

        public UniTask<Sprite> LoadIcon(CancellationTokenSource ct)
        {
            return guildVMF.RentImage(data.IconRef, ct);
        }
    }
}