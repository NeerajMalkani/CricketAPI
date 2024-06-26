﻿using CricketAPI.Entites;
using Microsoft.EntityFrameworkCore;

namespace CricketAPI.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options)
        {
        }

        public DbSet<Countries> Countries => Set<Countries>();
        public DbSet<Leagues> Leagues => Set<Leagues>();
        public DbSet<Seasons> Seasons => Set<Seasons>();
        public DbSet<Stages> Stages => Set<Stages>();
        public DbSet<Teams> Teams => Set<Teams>();
        public DbSet<Players> Players => Set<Players>();
        public DbSet<Venues> Venues => Set<Venues>();
        public DbSet<Officials> Officials => Set<Officials>();
        public DbSet<Fixtures> Fixtures => Set<Fixtures>();

        public DbSet<FixturesJson> FixturesJson => Set<FixturesJson>();
        public DbSet<ScorecardJson> ScorecardJson => Set<ScorecardJson>();
        public DbSet<LineupJson> LineupJson => Set<LineupJson>();
        public DbSet<BallJson> BallJson => Set<BallJson>();
        public DbSet<SeriesJson> SeriesJson => Set<SeriesJson>();
        public DbSet<StandingsJson> StandingsJson => Set<StandingsJson>();
        public DbSet<SeriesTeamsJson> SeriesTeamsJson => Set<SeriesTeamsJson>();
        public DbSet<ICCRankingsJson> ICCRankingsJson => Set<ICCRankingsJson>();
        public DbSet<PlayerBattingStatsJson> PlayerBattingStatsJson => Set<PlayerBattingStatsJson>();
        public DbSet<PlayerBowlingStatsJson> PlayerBowlingStatsJson => Set<PlayerBowlingStatsJson>();
        public DbSet<NewsJson> NewsJson => Set<NewsJson>();

        public DbSet<YouTubeVideos> YouTubeVideos => Set<YouTubeVideos>();

        public DbSet<UserContestMapping> UserContestMapping => Set<UserContestMapping>(); 
        public DbSet<UserTeam> UserTeam => Set<UserTeam>();
        public DbSet<UserTeamPlayers> UserTeamPlayers => Set<UserTeamPlayers>();
        public DbSet<UserTeamWithPlayersJson> UserTeamWithPlayersJson => Set<UserTeamWithPlayersJson>();
        public DbSet<UserAllTeamWithPlayersJson> UserAllTeamWithPlayersJson => Set<UserAllTeamWithPlayersJson>(); 
        public DbSet<UserTeamJson> UserTeamJson => Set<UserTeamJson>();
        public DbSet<UserTeamLineupJson> UserTeamLineupJson => Set<UserTeamLineupJson>();
        public DbSet<Contests> Contests => Set<Contests>();
        public DbSet<UserJoinedContests> UserJoinedContests => Set<UserJoinedContests>();
        public DbSet<ContestsJson> ContestsJson => Set<ContestsJson>();
        public DbSet<ContestsLeaderboardJson> ContestsLeaderboardJson => Set<ContestsLeaderboardJson>();
        public DbSet<UserTeamPointsJson> UserTeamPointsJson => Set<UserTeamPointsJson>();
        public DbSet<LastSeries> LastSeries => Set<LastSeries>();
        public DbSet<SeriesTeamPlayers> SeriesTeamPlayers => Set<SeriesTeamPlayers>();
        public DbSet<Users> Users => Set<Users>();
        public DbSet<Transactions> Transactions => Set<Transactions>();
        public DbSet<UserTeamPointsStats> UserTeamPointsStats => Set<UserTeamPointsStats>();
        public DbSet<FantasyPoints> FantasyPoints => Set<FantasyPoints>();
        public DbSet<GCMTokens> GCMTokens => Set<GCMTokens>(); 

    }
}
