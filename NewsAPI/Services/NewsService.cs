
using NewsAPI.Models;

namespace NewsAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpCient;
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";

        public NewsService( HttpClient httpClient)
        {
            _httpCient = httpClient;
        }

        public async Task<List<int>> GetTopStoriesAsync()
        {
            return await _httpCient.GetFromJsonAsync<List<int>>($"{BaseUrl}topstories.json") ?? new List<int>();
        }

        public async Task<NewsStory?> GetStoryByIdAsync(int id)
        {
            return await _httpCient.GetFromJsonAsync<NewsStory>($"{BaseUrl}item/{id}.json");
        }
    }
}
