using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class Countries
    {
        [Key]
        public int id { get; set; }
        public int continent_id { get; set; }
        public string? name { get; set; }
        public string? image_path { get; set; }
        public object? updated_at { get; set; }
    }
}
