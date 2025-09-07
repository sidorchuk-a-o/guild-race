using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class ClassPreviewComponent : MonoBehaviour
    {
        [SerializeField] private Image classIconImage;
        [SerializeField] private Image specIconImage;

        public async UniTask Init(CharacterVM characterVM, CancellationTokenSource ct)
        {
            var classIcon = await characterVM.ClassVM.LoadIcon(ct);
            var specIcon = await characterVM.SpecVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            classIconImage.sprite = classIcon;
            specIconImage.sprite = specIcon;
        }
    }
}