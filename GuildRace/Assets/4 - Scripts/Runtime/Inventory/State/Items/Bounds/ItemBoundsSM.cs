using Newtonsoft.Json;
using UnityEngine;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemBoundsSM
    {
        [ES3Serializable] private Vector3Int position;
        [ES3Serializable] private Vector3Int sizeWithoutRotation;
        [ES3Serializable] private bool isRotated;

        public ItemBoundsSM(ItemBoundsInfo info)
        {
            position = info.Position;
            sizeWithoutRotation = info.SizeWithoutRotation;
            isRotated = info.IsRotated;
        }

        public void ApplyValues(ItemBoundsInfo info)
        {
            info.SetPosition(position);
            info.SetSize(sizeWithoutRotation, isRotated);
        }
    }
}