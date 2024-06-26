﻿using CricketAPI.Entites;
using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class SeriesRepository
    {
        public List<Series> GetSeries(DataContext context)
        {
            List<Series> seriesLst = new List<Series>();
            try
            {
                List<SeriesJson> seriesJsons = context.SeriesJson.FromSqlRaw("CALL `cric_Get_Series`()").ToList();
                if (seriesJsons.Count > 0)
                {
                    seriesLst = JsonConvert.DeserializeObject<List<Series>>(seriesJsons[0].Series) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return seriesLst;
        }

        public List<Series> GetTrendingSeries(DataContext context)
        {
            List<Series> seriesLst = new List<Series>();
            try
            {
                List<SeriesJson> seriesJsons = context.SeriesJson.FromSqlRaw("CALL `cric_Get_TrendingSeries`()").ToList();
                if (seriesJsons.Count > 0)
                {
                    seriesLst = JsonConvert.DeserializeObject<List<Series>>(seriesJsons[0].Series) ?? throw new ArgumentException();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return seriesLst;
        }

        public List<Fixtures> GetSeriesFixtures(DataContext context, long series_id, long stage_id)
        {
            List<Fixtures> fixtures = new List<Fixtures>();
            try
            {
                List<FixturesJson> fixturesJson = context.FixturesJson.FromSqlRaw("CALL `cric_Get_SeriesFixtures_v1`(" + series_id + ", " + stage_id + ")").ToList();
                if (!fixturesJson[0].Fixtures.Equals("[]"))
                {
                    fixtures = JsonConvert.DeserializeObject<List<Fixtures>>(fixturesJson[0].Fixtures) ?? throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                fixtures = new List<Fixtures>();
            }
            return fixtures;
        }

        public List<Standings> GetStandings(DataContext context, long series_id, long stage_id)
        {
            List<Standings> standingsLst = new List<Standings>();
            try
            {
                List<StandingsJson> standingsJson = context.StandingsJson.FromSqlRaw("CALL `cric_Get_SeriesStandings_v1`(" + series_id + ", " + stage_id + ")").ToList();
                if (standingsJson.Count > 0)
                {
                    Standings standingsObj = JsonConvert.DeserializeObject<Standings>(standingsJson[0].Standings) ?? throw new ArgumentException();
                    if (standingsObj != null)
                    {
                        standingsLst.Add(standingsObj);
                    }
                }

            }
            catch (Exception)
            {
                standingsLst = new List<Standings>();
            }
            return standingsLst;
        }

        public List<SeriesTeams> GetSeriesTeams(DataContext context, long series_id)
        {
            List<SeriesTeams> teamsLst = new List<SeriesTeams>();
            try
            {
                List<SeriesTeamsJson> seriesTeamJson = context.SeriesTeamsJson.FromSqlRaw("CALL `cric_Get_SeriesTeams`(" + series_id + ")").ToList();
                if (seriesTeamJson.Count > 0)
                {
                    SeriesTeams teamObj = JsonConvert.DeserializeObject<SeriesTeams>(seriesTeamJson[0].SeriesTeams) ?? throw new ArgumentException();
                    if (teamObj != null)
                    {
                        teamsLst.Add(teamObj);
                    }
                }

            }
            catch (Exception)
            {
                teamsLst = new List<SeriesTeams>();
            }
            return teamsLst;
        }

        public List<LastSeries> GetLastSeries(DataContext context, long series_id)
        {
            List<LastSeries> lastSeries = new List<LastSeries>();
            try
            {
                lastSeries = context.LastSeries.FromSqlRaw("CALL `cric_Get_LastSeriesPoints`(" + series_id + ")").ToList();
            }
            catch (Exception)
            {
                lastSeries = new List<LastSeries>();
            }
            return lastSeries;
        }

        public int UpdateSeriesTeamPlayers(DataContext context, List<UpdateSeriesPoints> updateSeriesPoints)
        {
            int rowsAffected = 0;
            try
            {
                foreach (UpdateSeriesPoints updateSeries in updateSeriesPoints)
                {
                    SeriesTeamPlayers? seriesTeamPlayers = context.SeriesTeamPlayers.ToList().Where(el => el.season_id == updateSeries.series_id && el.player_id == updateSeries.player_id).FirstOrDefault();
                    if (seriesTeamPlayers != null)
                    {
                        seriesTeamPlayers.points = updateSeries.points;
                        context.SaveChanges();
                        rowsAffected = rowsAffected + 1;
                    }
                }

            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            return rowsAffected;
        }
    }
}
