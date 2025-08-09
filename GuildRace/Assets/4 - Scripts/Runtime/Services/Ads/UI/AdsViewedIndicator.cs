using UnityEngine;

namespace Game.Ads
{
    public class AdsViewedIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject indicator;

        public void SetState(bool state)
        {
            indicator.SetActive(state);
        }
    }
}