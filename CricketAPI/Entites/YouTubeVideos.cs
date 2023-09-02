using System.ComponentModel.DataAnnotations;

namespace CricketAPI.Entites
{
    public class YouTubeVideos
    {
        [Key]
        public string? id { get; set; }
        public string? video_url { get; set; }
        public string? channel_title { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? thumbnail { get; set; }
        public string? lengthSeconds { get; set; }
        public string? viewCount { get; set; }
        public DateTime? creationTime { get; set; } = DateTime.UtcNow;
    }
}
