using System.Threading;
using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Guild
{
    public class ClassVM : ViewModel
    {
        private readonly ClassData data;
        private readonly GuildVMFactory guildVMF;

        public ClassId Id { get; }
        public LocalizeKey NameKey { get; }

        public ClassVM(ClassData data, GuildVMFactory guildVMF)
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