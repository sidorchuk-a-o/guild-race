using System.Threading;
using UnityEngine;

namespace Game.Guild
{
    public class ClassContainer : MonoBehaviour
    {
        [SerializeField] private ClassItem classItem;
        [SerializeField] private SpecItem specItem;
        [SerializeField] private SubRoleItem subRoleItem;

        public void Init(CharacterVM characterVM, CancellationTokenSource ct)
        {
            classItem.Init(characterVM.ClassVM, ct);
            specItem.Init(characterVM.SpecVM, ct);
            subRoleItem.Init(characterVM.SpecVM.SubRoleVM, ct);
        }
    }
}