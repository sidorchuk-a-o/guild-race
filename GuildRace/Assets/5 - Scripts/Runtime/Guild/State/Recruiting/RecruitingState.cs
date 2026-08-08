using System;
using System.Collections.Generic;
using System.Linq;
using AD.Game.Inventory;
using AD.Management;
using AD.Save;
using R3;

namespace AD.Game.Guild
{
    public class RecruitingState : ServiceState<RecruitingSM>
    {
        private readonly GuildConfig config;
        private readonly IInventoryService inventoryService;

        private readonly ReactiveProperty<bool> isEnabled = new();
        private readonly JoinRequestsCollection requests = new(null);
        private readonly List<JoinRequestInfo> removedRequests = new();
        private readonly List<ClassRoleSelectorInfo> classRoleSelectors = new();

        public override string SaveKey => "recruiting";
        public override SaveSource SaveSource => SaveSource.App;

        public ReadOnlyReactiveProperty<bool> IsEnabled => isEnabled;

        public DateTime NextRequestTime { get; private set; }
        public IJoinRequestsCollection Requests => requests;
        public IReadOnlyList<ClassRoleSelectorInfo> ClassRoleSelectors => classRoleSelectors;

        public RecruitingState(GuildConfig config, IInventoryService inventoryService)
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

            if (!isEnabled.Value)
            {
                requests.RemoveAll(x => !x.IsDefault);
            }

            MarkAsDirty();
        }

        public void SetClassRoleSelectorState(RoleId roleId, bool isEnabled)
        {
            var weight = classRoleSelectors.First(x => x.RoleId == roleId);

            weight.SetActiveState(isEnabled);

            MarkAsDirty();
        }

        // == Save ==

        protected override RecruitingSM CreateSave()
        {
            var recruitingSM = new RecruitingSM
            {
                IsEnabled = IsEnabled.CurrentValue,
                NextRequestTime = NextRequestTime,
                ClassRoleSelectors = classRoleSelectors
            };

            recruitingSM.SetRequests(Requests, inventoryService);

            return recruitingSM;
        }

        protected override void CreateDefaultState()
        {
            classRoleSelectors.AddRange(CreateDefaultClassRoleSelectors());
        }

        protected override void ReadSave(RecruitingSM save)
        {
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