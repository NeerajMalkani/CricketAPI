using System.ComponentModel.DataAnnotations;

namespace CricketAPI
{
    public class Teams
    {
        [Key]
        public long id { get; set; }
        public string? name { get; set; }
        public string? code { get; set; }
        public long country_id { get; set; }
        public string? national_team { get; set; }
        public string? image_path { get; set; }
    }
}
