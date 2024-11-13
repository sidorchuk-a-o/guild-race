using System;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class CharacterParams
    {
        [SerializeField] private int power;
        [SerializeField] private int health;
        [SerializeField] private int resource;

        public int Power => power;
        public int Health => health;
        public int Resource => resource;
    }
}