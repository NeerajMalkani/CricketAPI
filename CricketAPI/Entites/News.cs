using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class News
    {
        [Key]
        public int id { get; set; }
        public string? hline { get; set; }
        public string? intro { get; set; }
        public string? source { get; set; }
        public string? context { get; set; }
        public string? pubTime { get; set; }
        public string? storyType { get; set; }
        public string? image_path { get; set; }
        public string? seoHeadline { get; set; }
    }

    public class NewsJson
    {
        [Key]
        public string News { get; set; } = "";
    }
}
