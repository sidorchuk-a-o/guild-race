using System.Threading;
using Cysharp.Threading.Tasks;
using AD.Services.Localization;
using AD.Services.Router;
using UnityEngine;

namespace Game.Guild
{
    public class RoleVM : ViewModel
    {
        private readonly RoleData data;
        private readonly GuildVMFactory guildVMF;

        public RoleId Id { get; }
        public LocalizeKey NameKey { get; }

        public RoleVM(RoleData data, GuildVMFactory guildVMF)
        {
            this.data = data;
            this.guildVMF = guildVMF;

            Id = data.Id;
            NameKey = data.NameKey;
        }

        protected override void InitSubscribes()
        {
        }

        public UniTask<Sprite> LoadIcon(CancellationTokenSource ct)
        {
            return guildVMF.RentImage(data.IconRef, ct);
        }
    }
}