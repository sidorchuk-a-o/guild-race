using System;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class BossRoomParams
    {
        [SerializeField] private GameObject enterDoor;
        [SerializeField] private GameObject exitDoor;

        public GameObject EnterDoor => enterDoor;
        public GameObject ExitDoor => exitDoor;
    }
}