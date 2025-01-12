using AD.Services.Router;
using UnityEngine;

namespace Game.Instances
{
    public class InstanceMapVM : ViewModel
    {
        private readonly InstanceMapComponent component;

        public InstanceMapVM(InstanceMapComponent component)
        {
            this.component = component;
        }

        protected override void InitSubscribes()
        {
        }

        public Vector2 GetEntryPoint(InstanceMapUIComponent map)
        {
            return RectUtils.GetMapRectPosition(map, component.EntryPoint.position);
        }
    }
}