using AD.UI;
using UnityEngine;

namespace AD.Game.Guild
{
    public class NicknameComponent : MonoBehaviour
    {
        [SerializeField] private UIText nameText;
        [SerializeField] private UIText surnameText;

        public void Init(CharacterVM characterVM)
        {
            nameText.SetTextParams(characterVM.NameKey);
            surnameText.SetTextParams(characterVM.SurnameKey);
        }
    }
}