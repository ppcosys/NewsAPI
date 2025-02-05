
using Microsoft.Extensions.Caching.Memory;
using NewsAPI.Dtos;
using NewsAPI.Models;

namespace NewsAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpCient;
        private readonly IMemoryCache _cache;
        private const string BaseUrl = "https://hacker-news.firebaseio.com/v0/";

        public NewsService( HttpClient httpClient, IMemoryCache cache)
        {
            _httpCient = httpClient;
            _cache = cache;
        }

        public async Task<List<int>> GetTopStoriesAsync()
        {
            return await _cache.GetOrCreateAsync("topstories", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return await _httpCient.GetFromJsonAsync<List<int>>($"{BaseUrl}topstories.json") ?? new List<int>();
            });
        }

        public async Task<NewsStory?> GetStoryByIdAsync(int id)
        {
            return await _cache.GetOrCreateAsync($"story_{id}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _httpCient.GetFromJsonAsync<NewsStory>($"{BaseUrl}item/{id}.json");
            });
        }

        public async Task<List<NewsStoryDto>> GetTopNBestStoriesAsync(int n)
        {
            var storyIds = await GetTopStoriesAsync();
            var topNIds = storyIds.Take(n).ToList();

            var tasks = topNIds.Select(id => GetStoryByIdAsync(id));
            var stories = await Task.WhenAll(tasks);

            return stories
                .Where(s => s != null)
                .OrderByDescending(s => s.Score)
                .Select(s => new NewsStoryDto
                {
                    Title = s.Title,
                    Uri = s.Url,
                    PostedBy = s.By,
                    Time = DateTimeOffset.FromUnixTimeSeconds(s.Time)
                        .ToOffset(TimeSpan.FromHours(1))
                        .DateTime,
                    Score = s.Score,
                    CommentCount = s.Descendants
                })
                .ToList();
        }
    }
}
