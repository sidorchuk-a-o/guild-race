using System.Threading;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class EquipGroupItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        public async UniTask Init(EquipGroupVM equipGroupVM, CancellationTokenSource ct)
        {
            var icon = await equipGroupVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            nameText.SetTextParams(equipGroupVM.NameKey);
        }
    }
}