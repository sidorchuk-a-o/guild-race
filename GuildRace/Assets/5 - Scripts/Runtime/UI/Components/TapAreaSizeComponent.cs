using UnityEngine;
using YG;

namespace AD.Game.UI
{
    public class TapAreaSizeComponent : MonoBehaviour
    {
        [SerializeField] private float scaleMod = 2f;

        private void Awake()
        {
            if (YG2.envir.isMobile)
            {
                transform.localScale = Vector3.one * scaleMod;
            }
        }
    }
}