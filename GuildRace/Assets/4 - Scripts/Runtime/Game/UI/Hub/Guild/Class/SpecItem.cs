using AD.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class SpecItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        public async void Init(SpecializationVM specVM, CancellationTokenSource ct)
        {
            var icon = await specVM.LoadIcon(ct.Token);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(specVM.NameKey);
        }
    }
}