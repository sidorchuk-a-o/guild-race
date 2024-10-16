﻿using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game
{
    public class RecruitingSettingsDialog : UIContainer
    {
        [Header("Class Roles")]
        [SerializeField] private List<ClassRoleSelector> classRoleSelectors;

        [Header("Switch State")]
        [SerializeField] private string startState;
        [SerializeField] private string stopState;
        [SerializeField] private UIButton switchStateButton;

        private RecruitingVM recruitingVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF)
        {
            recruitingVM = guildVMF.GetRecruiting();
        }

        private void Awake()
        {
            switchStateButton.OnClick
                .Subscribe(SwitchStateCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            recruitingVM.AddTo(disp);

            recruitingVM.IsEnabled
                .Subscribe(RecruitingStateChanged)
                .AddTo(disp);

            var classRoleSelectorsVM = recruitingVM.ClassRoleSelectorsVM;

            for (var i = 0; i < classRoleSelectorsVM.Count; i++)
            {
                var classRoleSelectorVM = classRoleSelectorsVM[i];
                var classRoleSelector = classRoleSelectors[i];

                classRoleSelector.Init(classRoleSelectorVM, disp);
            }
        }

        private void RecruitingStateChanged(bool state)
        {
            switchStateButton.SetState(state ? stopState : startState);
        }

        private void SwitchStateCallback()
        {
            recruitingVM.SwitchRecruitingState();
        }
    }
}