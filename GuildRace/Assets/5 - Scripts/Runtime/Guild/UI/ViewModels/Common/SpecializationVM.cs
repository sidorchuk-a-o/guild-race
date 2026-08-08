using System.Threading;
using AD.Localization;
using AD.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Guild
{
    public class SpecializationVM : ViewModel
    {
        private readonly SpecializationData data;
        private readonly GuildVMFactory guildVMF;

        public SpecializationId Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public RoleVM RoleVM { get; }
        public SubRoleVM SubRoleVM { get; }
        public AbilitiesVM AbilitiesVM { get; }

        public SpecializationVM(SpecializationData data, GuildVMFactory guildVMF)
        {
            this.data = data;
            this.guildVMF = guildVMF;

            Id = data.Id;
            NameKey = data.NameKey;
            DescKey = data.DescKey;

            RoleVM = guildVMF.GetRole(data.RoleId);
            SubRoleVM = guildVMF.GetSubRole(data.SubRoleId);
            AbilitiesVM = new(data.Abilities, guildVMF.InstancesVMF);
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