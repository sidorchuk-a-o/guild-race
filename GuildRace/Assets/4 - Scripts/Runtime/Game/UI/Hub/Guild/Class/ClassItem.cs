using AD.UI;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class ClassItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        public async void Init(ClassVM classVM, CancellationTokenSource ct)
        {
            var icon = await classVM.LoadIcon(ct.Token);

            if (ct.IsCancellationRequested)
            {
                return;
            }

            iconImage.sprite = icon;
            nameText.SetTextParams(classVM.NameKey);
        }
    }
}