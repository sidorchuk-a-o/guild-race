using AD.ToolsCollection;
using Game.Guild;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class SquadUnitsContainer : MonoBehaviour
    {
        [SerializeField] private List<SquadUnitSlot> unitSlots;

        private GuildVMFactory guildVMF;
        private IdsVM squadVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            this.guildVMF = guildVMF;
        }

        public void Init(ActiveInstanceVM activeInstanceVM, CompositeDisp disp)
        {
            squadVM = activeInstanceVM.SquadVM;

            squadVM.ObserveAdd()
                .Subscribe(x => AddCharacterCallback(x.Index, disp))
                .AddTo(disp);

            squadVM.ObserveRemove()
                .Subscribe(x => RemoveCharacterCallback(x.Value))
                .AddTo(disp);
        }

        private void AddCharacterCallback(int index, CompositeDisp disp)
        {
            var characterId = squadVM[index];
            var characterVM = guildVMF.GetCharacter(characterId.Value);

            characterVM.AddTo(disp);

            foreach (var unitSlot in unitSlots)
            {
                if (unitSlot.CharacterVM == null)
                {
                    unitSlot.SetCharacter(characterVM);

                    break;
                }
            }
        }

        private void RemoveCharacterCallback(IdVM characterIdVM)
        {
            var characterId = characterIdVM.Value;
            var unitSlot = unitSlots.FirstOrDefault(x => x.CharacterVM.Id == characterId);

            unitSlot.SetCharacter(null);
        }
    }
}