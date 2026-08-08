using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Instances
{
    public abstract class RewardItemContent : MonoBehaviour
    {
        public abstract UniTask Init(InstanceRewardVM rewardVM, CancellationTokenSource ct);
    }
}