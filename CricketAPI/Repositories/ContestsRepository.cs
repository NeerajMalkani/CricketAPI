using CricketAPI.Entites;
using CricketAPI.Helpers;

namespace CricketAPI.Repositories
{
    public class ContestsRepository
    {
        #region Prize Pool
        private Pools SetPoolObject(int remainingWinnigs, int sp, int ep, double per)
        {
            Pools pool = new Pools();
            pool.startPosition = sp;
            pool.endPosition = ep;
            pool.percentage = per;
            pool.prize = Convert.ToInt32(Math.Round(remainingWinnigs * Convert.ToDecimal(Convert.ToDecimal(per) / 100)));
            return pool;
        }

        public List<PrizePool> CalculatePrizePool(PrizePoolRequest prizePoolRequest)
        {
            List<PrizePool> prizePools = new List<PrizePool>();
            if (prizePoolRequest.entryFees > 0 && prizePoolRequest.spots > 1)
            {
                List<int> breakPoints = new List<int>() { 1, 2, 3, 4, 5, 7, 10, 15, 25, 50, 100, 250, 500, 1000, 2000, 5000 };
                int totalWinnings = prizePoolRequest.entryFees * prizePoolRequest.spots;
                int ssCommission = Convert.ToInt32(Math.Round(totalWinnings * Convert.ToDecimal(Convert.ToDecimal(20) / 100)));
                int remainingWinnigs = totalWinnings - ssCommission;

                int maximumWinners = Convert.ToInt32(Math.Round(prizePoolRequest.spots * (Convert.ToDecimal(50) / 100)));
                maximumWinners = breakPoints.OrderBy(item => Math.Abs(maximumWinners - item)).First();

                foreach (int i in breakPoints)
                {
                    if (i <= maximumWinners)
                    {
                        PrizePool prizePool = new PrizePool();
                        prizePool.breakPoint = i;
                        List<Pools> pools = new List<Pools>();
                        switch (i)
                        {
                            case 1:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 100));
                                break;
                            case 2:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 70));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 30));
                                break;
                            case 3:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 50));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 30));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 20));
                                break;
                            case 4:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 40));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 25));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 20));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 15));
                                break;
                            case 5:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 40));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 23));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 15));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 5, 11));
                                break;
                            case 7:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 35));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 19));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 12));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 7, 8));
                                break;
                            case 10:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 30));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 18));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 11));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 7.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 6));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 5.5));
                                break;
                            case 15:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 25));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 12.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 7.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 4));
                                break;
                            case 25:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 20));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 12));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 8));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 2.5));
                                break;
                            case 50:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 15));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 8));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 4));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 25, 1.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 26, 50, 1.1));
                                break;
                            case 100:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 15));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 8));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 3.75));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 3.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 1.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 15, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 16, 25, 0.6));
                                pools.Add(SetPoolObject(remainingWinnigs, 26, 100, 0.55));
                                break;
                            case 250:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 12));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 7.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 4, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 5, 5, 2.25));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 15, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 16, 25, 0.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 26, 50, 0.25));
                                pools.Add(SetPoolObject(remainingWinnigs, 50, 250, 0.22));
                                break;
                            case 500:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 10));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 7));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 3.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 5, 2.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 6, 10, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 25, 0.3));
                                pools.Add(SetPoolObject(remainingWinnigs, 26, 100, 0.2));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 250, 0.15));
                                pools.Add(SetPoolObject(remainingWinnigs, 251, 500, 0.11));
                                break;
                            case 1000:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 1.7));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 10, 0.9));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 50, 0.3));
                                pools.Add(SetPoolObject(remainingWinnigs, 51, 100, 0.2));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 500, 0.08));
                                pools.Add(SetPoolObject(remainingWinnigs, 501, 1000, 0.06));
                                break;
                            case 2000:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 5));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 3));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 10, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 50, 0.2));
                                pools.Add(SetPoolObject(remainingWinnigs, 51, 100, 0.1));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 500, 0.05));
                                pools.Add(SetPoolObject(remainingWinnigs, 501, 1000, 0.04));
                                pools.Add(SetPoolObject(remainingWinnigs, 1001, 2000, 0.03));
                                break;
                            case 5000:
                                pools.Add(SetPoolObject(remainingWinnigs, 1, 1, 4));
                                pools.Add(SetPoolObject(remainingWinnigs, 2, 2, 2));
                                pools.Add(SetPoolObject(remainingWinnigs, 3, 3, 1));
                                pools.Add(SetPoolObject(remainingWinnigs, 4, 10, 0.5));
                                pools.Add(SetPoolObject(remainingWinnigs, 11, 50, 0.25));
                                pools.Add(SetPoolObject(remainingWinnigs, 51, 100, 0.1));
                                pools.Add(SetPoolObject(remainingWinnigs, 101, 500, 0.05));
                                pools.Add(SetPoolObject(remainingWinnigs, 501, 1000, 0.021));
                                pools.Add(SetPoolObject(remainingWinnigs, 1001, 5000, 0.011));
                                break;
                        }
                        prizePool.pools = pools;
                        prizePools.Add(prizePool);
                    }
                    else
                    {
                        break;
                    }
                }
            }


            return prizePools;
        }
        #endregion

        #region Contests
        public async Task<int> InsertContests (DataContext context, Contests contests)
        {
            int rowsAffected = 0;
            context.Contests.Add(contests);
            await context.SaveChangesAsync();
            return rowsAffected;
        }

        public List<Contests> GetContests(DataContext context, long fixture_id)
        {
            List<Contests> contests = new List<Contests>();
            try
            {
                contests = context.Contests.Where(el => el.fixture_id == fixture_id).ToList();
            }
            catch (Exception)
            {
                contests = new List<Contests>();
            }
            return contests;
        }
        #endregion
    }
}
