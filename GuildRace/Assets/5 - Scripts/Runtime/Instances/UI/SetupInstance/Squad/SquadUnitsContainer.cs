using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using R3;
using UnityEngine;

namespace AD.Game.Instances
{
    public class SquadUnitsContainer : MonoBehaviour
    {
        [SerializeField] private List<SquadUnitSlot> unitSlots;

        private SquadUnitsVM squadVM;

        public void Init(ActiveInstanceVM activeInstanceVM, CompositeDisp disp)
        {
            squadVM = activeInstanceVM.SquadVM;

            squadVM.ObserveAdd()
                .Subscribe(x => AddCharacterCallback(x.Index, disp))
                .AddTo(disp);

            squadVM.ObserveRemove()
                .Subscribe(x => RemoveCharacterCallback(x.Value, disp))
                .AddTo(disp);

            for (var i = 0; i < unitSlots.Count; i++)
            {
                var slot = unitSlots[i];
                var squadUnitVM = squadVM.ElementAtOrDefault(i);

                slot.SetSquadUnit(squadUnitVM, disp);
                slot.SetActive(i < squadVM.MaxUnitsCount);
            }
        }

        private void AddCharacterCallback(int index, CompositeDisp disp)
        {
            var squadUnitVM = squadVM[index];
            var filteredSlots = unitSlots.Where(slot => !slot.HasUnit);

            foreach (var unitSlot in filteredSlots)
            {
                unitSlot.SetSquadUnit(squadUnitVM, disp);
                break;
            }
        }

        private void RemoveCharacterCallback(SquadUnitVM squadUnitVM, CompositeDisp disp)
        {
            var filteredSlots = unitSlots.Where(slot =>
            {
                return slot.HasUnit
                    && slot.SquadUnitVM.CharactedId == squadUnitVM.CharactedId;
            });

            foreach (var unitSlot in filteredSlots)
            {
                unitSlot.SetSquadUnit(null, disp);
                break;
            }
        }
    }
}