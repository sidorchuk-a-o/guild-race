using AD.Services.Save;
using AD.States;
using Game.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using VContainer;

namespace Game.Guild
{
    public class RecruitingState : State<RecruitingSM>
    {
        private readonly GuildConfig config;
        private readonly IInventoryService inventoryService;

        private readonly ReactiveProperty<bool> isEnabled = new();
        private readonly JoinRequestsCollection requests = new(null);
        private readonly List<JoinRequestInfo> removedRequests = new();
        private readonly List<ClassRoleSelectorInfo> classRoleSelectors = new();

        public override string SaveKey => RecruitingSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IReadOnlyReactiveProperty<bool> IsEnabled => isEnabled;

        public DateTime NextRequestTime { get; private set; }
        public IJoinRequestsCollection Requests => requests;
        public IReadOnlyList<ClassRoleSelectorInfo> ClassRoleSelectors => classRoleSelectors;

        public RecruitingState(
            GuildConfig config,
            IInventoryService inventoryService,
            IObjectResolver resolver)
            : base(resolver)
        {
            this.config = config;
            this.inventoryService = inventoryService;
        }

        public void SetNextRequestTime(DateTime value)
        {
            NextRequestTime = value;
        }

        public void AddRequest(JoinRequestInfo request)
        {
            requests.Add(request);

            MarkAsDirty();
        }

        public int RemoveRequest(string requestId)
        {
            var index = requests.FindIndex(x => x.Id == requestId);

            if (index != -1)
            {
                var request = requests[index];

                requests.RemoveAt(index);
                removedRequests.Add(request);

                MarkAsDirty();
            }

            return index;
        }

        public JoinRequestInfo GetRemovedRequest(string requestId)
        {
            var request = removedRequests.FirstOrDefault(x => x.Id == requestId);

            removedRequests.Clear();

            return request;
        }

        public void SwitchRecruitingState()
        {
            isEnabled.Value = !isEnabled.Value;

            if (isEnabled.Value == false)
            {
                requests.RemoveAll(x => !x.IsDefault);
            }

            MarkAsDirty();
        }

        public void SetClassRoleSelectorState(RoleId roleId, bool isEnabled)
        {
            var weight = classRoleSelectors.FirstOrDefault(x => x.RoleId == roleId);

            weight.SetActiveState(isEnabled);

            MarkAsDirty();
        }

        // == Save ==

        protected override RecruitingSM CreateSave()
        {
            var recruitingSM = new RecruitingSM
            {
                IsEnabled = IsEnabled.Value,
                NextRequestTime = NextRequestTime,
                ClassRoleSelectors = classRoleSelectors
            };

            recruitingSM.SetRequests(Requests, inventoryService);

            return recruitingSM;
        }

        protected override void ReadSave(RecruitingSM save)
        {
            if (save == null)
            {
                classRoleSelectors.AddRange(CreateDefaultClassRoleSelectors());

                return;
            }

            isEnabled.Value = save.IsEnabled;
            NextRequestTime = save.NextRequestTime;

            requests.AddRange(save.GetRequests(inventoryService));
            classRoleSelectors.AddRange(save.ClassRoleSelectors);
        }

        private IEnumerable<ClassRoleSelectorInfo> CreateDefaultClassRoleSelectors()
        {
            return config.CharactersParams.Roles.Select(x =>
            {
                return new ClassRoleSelectorInfo(x.Id, isEnabled: true);
            });
        }
    }
}