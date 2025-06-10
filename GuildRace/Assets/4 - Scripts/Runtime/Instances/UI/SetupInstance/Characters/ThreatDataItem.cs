using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
{
    public class ThreatDataItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        public async UniTask Init(ThreatDataVM threatVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            var icon = await threatVM.LoadIcon(ct);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(threatVM.NameKey);
        }
    }
}