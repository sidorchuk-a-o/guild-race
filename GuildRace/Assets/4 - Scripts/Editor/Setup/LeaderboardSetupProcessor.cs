using Game.Guild;
using AD.ToolsCollection;
using AD.Services.Localization;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using YG.Utils.LB;
using YG;

namespace AD.Services.Leaderboards
{
    public class LeaderboardSetupProcessor
    {
        [MenuItem("Tools/YG2/Generate Leaderbord")]
        public static void SetupLeaderboardData()
        {
            var lBDatas = new LBData[]
            {
                new()
                {
                    technoName = "GuildScoreSeason1",
                    entries = "1. 1: 100\n2. 2: 90\n3. 3: 80\n4. 4: 70\n5. 5: 60\n6. 6: 50\n7. 7: 40\n8. 8: 30\n9. 9: 20\n10. 10: 10",
                    type = "numeric",
                    players = new LBPlayerData[]
                    {
                        new() { rank = 1, score = 550, uniqueID = "123" },
                        new() { rank = 2, score = 400, uniqueID = "321" },
                        new() { rank = 3, score = 345, uniqueID = "456" },
                        new() { rank = 4, score = 250, uniqueID = "321" },
                        new() { rank = 5, score = 210, uniqueID = "330" },
                        new() { rank = 6, score = 185, uniqueID = "326" },
                        new() { rank = 7, score = 110, uniqueID = "327" },
                        new() { rank = 8, score = 70, uniqueID = "328" },
                        new() { rank = 9, score = 50, uniqueID = "329" },
                        new() { rank = 10, score = 0, uniqueID = "000" }
                    },
                    currentPlayer = new LBCurrentPlayerData
                    {
                        rank = 10,
                        score = 0
                    }
                },
                new()
                {
                    technoName = "GuildPowerSeason1",
                    entries = "1. 1: 100\n2. 2: 90\n3. 3: 80\n4. 4: 70\n5. 5: 60\n6. 6: 50\n7. 7: 40\n8. 8: 30\n9. 9: 20\n10. 10: 10",
                    type = "numeric",
                    players = new LBPlayerData[]
                    {
                        new() { rank = 1, score = 432, uniqueID = "123" },
                        new() { rank = 2, score = 410, uniqueID = "321" },
                        new() { rank = 3, score = 358, uniqueID = "456" },
                        new() { rank = 4, score = 254, uniqueID = "321" },
                        new() { rank = 5, score = 237, uniqueID = "330" },
                        new() { rank = 6, score = 197, uniqueID = "326" },
                        new() { rank = 7, score = 186, uniqueID = "327" },
                        new() { rank = 8, score = 150, uniqueID = "328" },
                        new() { rank = 9, score = 100, uniqueID = "329" },
                        new() { rank = 10, score = 40, uniqueID = "000" }
                    },
                    currentPlayer = new LBCurrentPlayerData
                    {
                        rank = 10,
                        score = 40
                    }
                }
            };

            FillExtraData(lBDatas);

            InfoYG.Inst().Leaderboards.listLBSim = lBDatas;
            InfoYG.Inst().MarkAsDirty();
        }

        private static void FillExtraData(LBData[] lbDatas)
        {
            var extraDatas = GetExtraDatas();

            foreach (var lbData in lbDatas)
            {
                for (var i = 0; i < lbData.players.Length; i++)
                {
                    var player = lbData.players[i];
                    var extraData = extraDatas[i];

                    player.extraData = extraData;
                }
            }
        }

        private static string[] GetExtraDatas()
        {
            var langCode = LocalizationEditorState.Config.DefaultLanguageCode;
            var guildNames = guildNamesDict[langCode];

            var guilds = guildNames.Select((x, i) => new GuildScoreData
            {
                GuildName = x,
                Emblem = guildEmblems[i]
            });

            return guilds
                .Select(x => JsonConvert.SerializeObject(x))
                .ToArray();
        }

        private static readonly Dictionary<string, string[]> guildNamesDict = new()
        {
            ["ru"] = new string[]
            {
                "Дух Шторма",
                "Авангард Судьбы",
                "Дети Рассвета",
                "Хранители Бездны",
                "Потусторонний Совет",
                "Железный Улей",
                "Клан Громового Молота",
                "Орден Белой Розы",
                "Витязи Древней Клятвы",
                "Спящий Легион", // ***
            },
            ["en"] = new string[]
            {
                "Aeterna Imperium",
                "Sovereign Legacy",
                "Dawnbringers",
                "Arcane Syndicate",
                "Echoes of Fate",
                "Veil Walkers",
                "Bloodstone Marauders",
                "Warhound Pack",
                "Oblivion Circle", 
                "Ascendant Order", // ***
            }
        };

        private static readonly List<EmblemInfo> guildEmblems = new()
        {
            CreateEmblem(3, 19, 0, new[] { 5 }),
            CreateEmblem(6, 15, 14, new[] { 3, 12, 5 }),
            CreateEmblem(4, 0, 2, new[] { 18, 5 }),
            CreateEmblem(11, 15, 3, new[] { 9, 1 }),
            CreateEmblem(8, 1, 1, new[] { 3, 5 }),
            CreateEmblem(23, 17, 12, new[] { 16, 5, 20 }),
            CreateEmblem(37, 19, 15, new[] { 0, 10, 13 }),
            CreateEmblem(1, 18, 21, new[] { 3, 15, 3 }),
            CreateEmblem(35, 16, 18, new[] { 13, 18, 13 }),
            CreateEmblem(27, 1, 20, new[] { 1, 4, 5 }),     // 28, 2, 21, 2, 5, 6
        };

        private static EmblemInfo CreateEmblem(int sym, int symc, int bg, int[] bgc)
        {
            var emblem = new EmblemInfo();

            emblem.SetBackground(bg);
            emblem.SetBackgroundColors(bgc);
            emblem.SetSymbol(sym);
            emblem.SetSymbolColor(symc);

            return emblem;
        }
    }
}