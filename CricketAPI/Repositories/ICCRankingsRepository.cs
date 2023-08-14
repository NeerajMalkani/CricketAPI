using CricketAPI.Entites;
using CricketAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CricketAPI.Repositories
{
    public class ICCRankingsRepository
    {
        public List<ICCRankings> GetICCRankings(DataContext context, string type, string gender)
        {
            List<ICCRankings> iccRankingsLst = new List<ICCRankings>();
            try
            {
                List<ICCRankingsJson> iccRankingsJson = context.ICCRankingsJson.FromSqlRaw("CALL `cric_Get_ICCRankings`('" + type + "', '" + gender + "')").ToList();
                if (iccRankingsJson.Count > 0)
                {
                    ICCRankings iccRankingsObj = JsonConvert.DeserializeObject<ICCRankings>(iccRankingsJson[0].ICCRankings) ?? throw new ArgumentException();
                    if (iccRankingsObj != null)
                    {
                        iccRankingsLst.Add(iccRankingsObj);
                    }
                }

            }
            catch (Exception)
            {
                iccRankingsLst = new List<ICCRankings>();
            }
            return iccRankingsLst;
        }
    }
}
