using System.Threading;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Craft
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