using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Craft
{
    public class RecycleReagentItem : MonoBehaviour
    {
        [SerializeField] private Image reagentIconImage;
        [SerializeField] private UIText reagentCountText;

        public async UniTask Init(RecyclingItemVM itemVM)
        {
            reagentIconImage.sprite = await itemVM.ReagentVM.LoadIcon();

            reagentCountText.SetTextParams(itemVM.RecyclingResult);
        }
    }
}