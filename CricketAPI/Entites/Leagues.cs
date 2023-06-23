using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class Leagues
    {
        [Key]
        public int id { get; set; }
        public int season_id { get; set; }
        public int? country_id { get; set; }
        public string? name { get; set; }
        public string? code { get; set; }
        public string? image_path { get; set; }
        public string? type { get; set; }
        public DateTime updated_at { get; set; }
    }
}
