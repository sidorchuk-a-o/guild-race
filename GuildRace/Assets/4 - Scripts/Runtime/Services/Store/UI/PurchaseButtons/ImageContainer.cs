using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Store
{
    public class ImageContainer : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void SetImage(Sprite value)
        {
            image.sprite = value;

            this.SetActive(value);
        }
    }
}