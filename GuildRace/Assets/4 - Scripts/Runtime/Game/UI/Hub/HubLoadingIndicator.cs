using UnityEngine;

namespace Game
{
    public class HubLoadingIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject loading;
        [SerializeField] private GameObject content;

        private void Update()
        {
            var check = content.transform.childCount <= 1;

            if (check && loading.activeSelf)
            {
                return;
            }

            if (!check && !loading.activeSelf)
            {
                return;
            }

            loading.SetActive(check);
        }
    }
}