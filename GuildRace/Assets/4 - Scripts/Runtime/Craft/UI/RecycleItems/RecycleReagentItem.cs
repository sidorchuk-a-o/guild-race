using AD.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Craft
{
    public class RecycleReagentItem : MonoBehaviour
    {
        [SerializeField] private Image reagentIconImage;
        [SerializeField] private UIText reagentCountText;

        public async UniTask Init(RecyclingItemVM itemVM, CancellationTokenSource ct)
        {
            var icon = await itemVM.ReagentVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            reagentIconImage.sprite = icon;
            reagentCountText.SetTextParams(itemVM.RecyclingResult);
        }
    }
}