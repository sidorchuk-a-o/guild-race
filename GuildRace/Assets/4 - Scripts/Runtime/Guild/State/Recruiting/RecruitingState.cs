using AD.Services.Save;
using AD.States;
using Game.Items;
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
        private readonly IItemsDatabaseService itemsService;

        private readonly ReactiveProperty<bool> isEnabled = new();
        private readonly JoinRequestsCollection requests = new(null);
        private readonly List<ClassRoleSelectorInfo> classRoleSelectors = new();

        public override string SaveKey => RecruitingSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IReadOnlyReactiveProperty<bool> IsEnabled => isEnabled;

        public DateTime NextRequestTime { get; private set; }
        public IJoinRequestsCollection Requests => requests;
        public IReadOnlyList<ClassRoleSelectorInfo> ClassRoleSelectors => classRoleSelectors;

        public RecruitingState(
            GuildConfig config,
            IItemsDatabaseService itemsService,
            IObjectResolver resolver)
            : base(resolver)
        {
            this.config = config;
            this.itemsService = itemsService;
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

            requests.RemoveAt(index);

            MarkAsDirty();

            return index;
        }

        public void SwitchRecruitingState()
        {
            isEnabled.Value = !isEnabled.Value;

            if (isEnabled.Value == false)
            {
                requests.Clear();
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

            recruitingSM.SetRequests(Requests, itemsService);

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

            requests.AddRange(save.GetRequests(itemsService));
            classRoleSelectors.AddRange(save.ClassRoleSelectors);
        }

        private IEnumerable<ClassRoleSelectorInfo> CreateDefaultClassRoleSelectors()
        {
            return config.CharactersModule.Roles.Select(x =>
            {
                return new ClassRoleSelectorInfo(x.Id, isEnabled: true);
            });
        }
    }
}