using NewsAPI.Dtos;
using NewsAPI.Models;

namespace NewsAPI.Services
{
    public interface INewsService
    {
        Task<List<int>> GetTopStoriesAsync();
        Task<NewsStory?> GetStoryByIdAsync(int id);
        Task<List<NewsStoryDto>> GetTopNBestStoriesAsync(int n);
    }
}
