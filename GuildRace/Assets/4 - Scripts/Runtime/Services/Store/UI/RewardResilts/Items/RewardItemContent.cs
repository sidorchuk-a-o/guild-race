using System.Threading;
using AD.Services.Store;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Store
{
    public abstract class RewardItemContent : MonoBehaviour
    {
        public abstract UniTask Init(RewardResultVM rewardVM, CancellationTokenSource ct);
    }
}