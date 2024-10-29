using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Guild
{
    [Serializable]
    public class GuildBankParams
    {
        [SerializeField] private List<GuildBankTabData> tabs;

        public IReadOnlyList<GuildBankTabData> Tabs => tabs;

        public GuildBankTabData GetTab(string id)
        {
            return tabs.FirstOrDefault(x => x.Id == id);
        }
    }
}