using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class Ranking
    {
        public Teams? team { get; set; }
        public int points { get; set; } = 0;
        public int rating { get; set; } = 0;
        public int matches { get; set; } = 0;
        public int position { get; set; } = 0;
    }

    public class Teams
    {
        [Key]
        public int id { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
    }

    public class ICCRankings
    {
        public string? type { get; set; }
        public string? gender { get; set; }
        public List<Ranking>? rankings { get; set; }
        public DateTime? updated_at { get; set; }
    }

    public class ICCRankingsJson
    {
        [Key]
        public string ICCRankings { get; set; } = "";
    }
}
