using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class Officials
    {
        [Key]
        public int id { get; set; }
        public int country_id { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? fullname { get; set; }
        public string? dateofbirth { get; set; }
        public string? gender { get; set; }
        public DateTime updated_at { get; set; }
    }
}
