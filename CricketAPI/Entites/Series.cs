using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class Series
    {
        [Key]
        public long id { get; set; }
        public string? image_path { get; set; }
        public string? league_type { get; set; }
        public string? series_name { get; set; }
        public DateTime? starting_at { get; set; }
    }
    public class SeriesJson
    {
        [Key]
        public string Series { get; set; } = "";
    }
}
