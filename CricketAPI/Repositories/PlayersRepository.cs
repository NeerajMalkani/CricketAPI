using CricketAPI.Entites;
using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class PlayersRepository
    {
        public List<Players> GetPlayerInfo(DataContext context, long id)
        {
            List<Players> players = new List<Players>();
            try
            {
                players = context.Players.Where(pl => pl.id == id).ToList();
            }
            catch (Exception)
            {
                players = new List<Players>();
            }
            return players;
        }

        public List<Players> GetSearchPlayer(DataContext context, string playername)
        {
            List<Players> players = new List<Players>();
            try
            {
                players = context.Players.Where(pl => (pl.fullname != null && pl.fullname.Contains(playername))).ToList();
            }
            catch (Exception)
            {
                players = new List<Players>();
            }
            return players;
        }

        public List<PlayerBattingStats> GetPlayerBattingStats(DataContext context, long series_id, long player_id)
        {
            List<PlayerBattingStats> playerBattingLst = new List<PlayerBattingStats>();
            try
            {
                List<PlayerBattingStatsJson> playerBattingJson = context.PlayerBattingStatsJson.FromSqlRaw("CALL `cric_Get_PlayerBattingStats`(" + series_id + "," + player_id + ")").ToList();
                if (playerBattingJson.Count > 0)
                {
                    PlayerBattingStats playerBattingObj = JsonConvert.DeserializeObject<PlayerBattingStats>(playerBattingJson[0].PlayerBattingStats) ?? throw new ArgumentException();
                    if (playerBattingObj != null)
                    {
                        playerBattingLst.Add(playerBattingObj);
                    }
                }

            }
            catch (Exception)
            {
                playerBattingLst = new List<PlayerBattingStats>();
            }
            return playerBattingLst;
        }

        public List<PlayerBowlingStats> GetPlayerBowlingStats(DataContext context, long series_id, long player_id)
        {
            List<PlayerBowlingStats> playerBowlingLst = new List<PlayerBowlingStats>();
            try
            {
                List<PlayerBowlingStatsJson> playerBowlingJson = context.PlayerBowlingStatsJson.FromSqlRaw("CALL `cric_Get_PlayerBowlingStats`(" + series_id + "," + player_id + ")").ToList();
                if (playerBowlingJson.Count > 0)
                {
                    PlayerBowlingStats playerBowlingObj = JsonConvert.DeserializeObject<PlayerBowlingStats>(playerBowlingJson[0].PlayerBowlingStats) ?? throw new ArgumentException();
                    if (playerBowlingObj != null)
                    {
                        playerBowlingLst.Add(playerBowlingObj);
                    }
                }

            }
            catch (Exception)
            {
                playerBowlingLst = new List<PlayerBowlingStats>();
            }
            return playerBowlingLst;
        }
    }
}
