using Game.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances
{
    public class InstanceMapComponent : GameComponent<InstanceMapComponent>
    {
        [Header("Navigation")]
        [SerializeField] private TextAsset graphData;

        [Header("Rooms")]
        [SerializeField] private Transform entryPoint;
        [SerializeField] private List<BossRoomParams> bossRooms;

        public Transform EntryPoint => entryPoint;

        public void Init()
        {
            LoadGraphData();

            InitBossRooms();
        }

        public void LoadGraphData()
        {
            AstarPath.active.data.DeserializeGraphs(graphData.bytes);
        }

        private void InitBossRooms()
        {
            for (var i = 0; i < bossRooms.Count; i++)
            {
                UnlockEnterDoor(i);
            }
        }

        // == Boss Room - Doors ==

        public void LockEnterDoor(int index)
        {
            var room = bossRooms[index];

            if (room.EnterDoor)
            {
                room.EnterDoor.SetActive(true);
            }
        }

        public void UnlockEnterDoor(int index)
        {
            var room = bossRooms[index];

            if (room.EnterDoor)
            {
                room.EnterDoor.SetActive(false);
            }
        }

        public void UnlockExitDoor(int index)
        {
            var room = bossRooms[index];

            if (room.ExitDoor)
            {
                room.ExitDoor.SetActive(false);
            }
        }
    }
}