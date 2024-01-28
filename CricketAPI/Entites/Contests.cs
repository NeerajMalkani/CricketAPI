﻿namespace CricketAPI.Entites
{
    #region Prize Pool
    public class PrizePoolRequest
    {
        public int entryFees { get; set; }
        public int spots { get; set; }
    }
    public class PrizePool
    {
        public int breakPoint { get; set; }
        public List<Pools>? pools { get; set; }
    }
    public class Pools
    {
        public int startPosition { get; set; }
        public int endPosition { get; set; }
        public double percentage { get; set; }
        public int prize { get; set; }
    }
    #endregion

    #region Contests
    public class Contests
    {
        public long id { get; set; }
        public string? created_by { get; set; }
        public long fixture_id { get; set; }
        public string? contest_name { get; set; }
        public int entry_fees { get; set; }
        public int spots { get; set; }
        public int spots_filled { get; set; }
        public int number_of_winners { get; set; }
    }
    #endregion
}
