using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Instances
{
    public abstract class RewardItemContent : MonoBehaviour
    {
        public abstract UniTask Init(InstanceRewardVM rewardVM, CancellationTokenSource ct);
    }
}