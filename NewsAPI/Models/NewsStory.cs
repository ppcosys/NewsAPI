namespace NewsAPI.Models
{
    public class NewsStory
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string By { get; set; } = string.Empty;
        public int Score { get; set; }
        public long Time { get; set; }
        public int Descendants { get; set; }
    }
}
