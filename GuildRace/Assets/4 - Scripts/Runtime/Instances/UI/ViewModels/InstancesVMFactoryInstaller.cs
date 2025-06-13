using System.Collections.Generic;
using AD.Services.Router;
using UnityEngine;

namespace Game.Instances
{
    public class InstancesVMFactoryInstaller : VMFactoryInstaller<InstancesVMFactory>
    {
        [SerializeField] private List<ConsumableMechanicVMFactory> mechanicFactories;
        [SerializeField] private List<RewardVMFactory> rewardsFactories;

        protected override void PostInstall()
        {
            base.PostInstall();

            Instance.SetConsumableMechanicFactories(mechanicFactories);
            Instance.SetRewardFactories(rewardsFactories);
        }
    }
}