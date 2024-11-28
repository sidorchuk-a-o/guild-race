using AD.ToolsCollection;
using Game.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Instances
{
    public class InstanceMapUIComponent : UIComponent<InstanceMapUIComponent>
    {
        [Header("Image")]
        [SerializeField] private Image mapImage;

        [Header("Rooms")]
        [SerializeField] private List<BossRoomUIParams> bossRooms;

        private InstancesVMFactory instancesVMF;
        private InstanceMapVM mapVM;

        public Image MapImage => mapImage;
        public RectTransform EntryPoint { get; private set; }

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        public void Init(CompositeDisp disp)
        {
            mapVM = instancesVMF.GetInstanceMap();
            mapVM.AddTo(disp);

            CreateEntryPoint();

            InitBossRooms();
        }

        private void CreateEntryPoint()
        {
            var pointGO = new GameObject("EntryPoint", typeof(RectTransform));
            var pointRect = pointGO.transform as RectTransform;

            pointGO.SetParent(gameObject);

            pointRect.sizeDelta = Vector2.zero;
            pointRect.anchorMax = Vector2.zero;
            pointRect.anchorMin = Vector2.zero;
            pointRect.pivot = Vector2.zero;

            pointRect.anchoredPosition = mapVM.GetEntryPoint(this);

            EntryPoint = pointRect;
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