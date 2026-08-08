using System.Threading;
using AD.Localization;
using AD.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Guild
{
    public class SubRoleVM : ViewModel
    {
        private readonly SubRoleData data;
        private readonly GuildVMFactory guildVMF;

        public SubRoleId Id { get; }
        public LocalizeKey NameKey { get; }

        public SubRoleVM(SubRoleData data, GuildVMFactory guildVMF)
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