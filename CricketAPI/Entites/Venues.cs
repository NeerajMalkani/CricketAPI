using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class Venues
    {
        [Key]
        public int id { get; set; }
        public int? country_id { get; set; }
        public string? name { get; set; }
        public string? city { get; set; }
        public string? image_path { get; set; }
        public int? capacity { get; set; }
        public bool? floodlight { get; set; }
        public DateTime updated_at { get; set; }
    }
}
