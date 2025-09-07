#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.UI;
using UnityEngine;

namespace Game.Guild
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