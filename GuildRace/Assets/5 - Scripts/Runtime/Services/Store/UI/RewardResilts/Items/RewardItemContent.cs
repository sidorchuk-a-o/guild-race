using System.Threading;
using AD.Store;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Store
{
    public abstract class RewardItemContent : MonoBehaviour
    {
        public abstract UniTask Init(RewardResultVM rewardVM, CancellationTokenSource ct);
    }
}