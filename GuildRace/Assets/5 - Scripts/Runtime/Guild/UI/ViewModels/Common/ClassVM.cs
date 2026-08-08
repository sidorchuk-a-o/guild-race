using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AD.Game.Inventory;
using AD.Localization;
using AD.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Guild
{
    public class ClassVM : ViewModel
    {
        private readonly ClassData data;
        private readonly GuildVMFactory guildVMF;

        public ClassId Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public EquipTypeVM ArmorTypeVM { get; }
        public EquipTypeVM WeaponTypeVM { get; }

        public List<SpecializationVM> SpecializationsVM { get; }

        public ClassVM(ClassData data, GuildVMFactory guildVMF)
        {
            this.data = data;
            this.guildVMF = guildVMF;

            Id = data.Id;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            ArmorTypeVM = guildVMF.InventoryVMF.GetEquipType(data.ArmorType);
            WeaponTypeVM = guildVMF.InventoryVMF.GetEquipType(data.WeaponType);

            SpecializationsVM = data.Specs
                .Select(x => guildVMF.GetSpecialization(x.Id))
                .ToList();
        }

        protected override void InitSubscribes()
        {
            ArmorTypeVM.AddTo(this);
            WeaponTypeVM.AddTo(this);
            SpecializationsVM.ForEach(x => x.AddTo(this));
        }

        public UniTask<Sprite> LoadIcon(CancellationTokenSource ct)
        {
            return guildVMF.RentImage(data.IconRef, ct);
        }
    }
}