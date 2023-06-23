using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class Seasons
    {
        [Key]
        public int id { get; set; }
        public int league_id { get; set; }
        public string? name { get; set; }
        public string? code { get; set; }
        public DateTime updated_at { get; set; }
    }
}
