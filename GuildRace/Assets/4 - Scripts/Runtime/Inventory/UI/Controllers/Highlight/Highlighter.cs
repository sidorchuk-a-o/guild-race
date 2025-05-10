using AD.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class Highlighter : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private UIStates states;

        private int cellSize;

        public void SetParent(Transform parent, int cellSize)
        {
            this.cellSize = cellSize;

            transform.SetParent(parent);
        }

        public void Show(bool state)
        {
            gameObject.SetActive(state);
        }

        public void SetState(string state)
        {
            states.SetState(state);
        }

        public void SetBounds(in BoundsInt bounds)
        {
            transform.ApplyItemBounds(bounds, cellSize);
        }
    }
}