using System.ComponentModel.DataAnnotations;

namespace CricketAPI
{
    public class Teams
    {
        [Key]
        public long teamId { get; set; }
        public string? teamName { get; set; }
        public string? teamSName { get; set; }
        public short teamType { get; set; }
        public long imageId { get; set; }
    }

    public class TeamsRequest
    {
        /// <summary>
        /// 1- International, 2 - League, 3 - Domestic, 4- Women
        /// </summary>
        public short teamType { get; set; }
    }
}
