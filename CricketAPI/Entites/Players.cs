namespace CricketAPI.Entites
{
    public class Players
    {
        public int id { get; set; }
        public int country_id { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? fullname { get; set; }
        public string? image_path { get; set; }
        public string? dateofbirth { get; set; }
        public string? gender { get; set; }
        public string? battingstyle { get; set; }
        public string? bowlingstyle { get; set; }
        public string? positionresource { get; set; }
        public int? positionid { get; set; }
        public string? positionname { get; set; }
        public DateTime updated_at { get; set; }
    }
}
