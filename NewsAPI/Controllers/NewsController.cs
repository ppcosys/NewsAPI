using Microsoft.AspNetCore.Mvc;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }


        [HttpGet("best-n-stories")]
        public async Task<IActionResult> GetBestNStories([FromQuery] int n)
        {
            var stories = await _newsService.GetTopNBestStoriesAsync(n);

            return Ok(stories);
        }

        [HttpGet("best-n-stories-paginated")]
        public async Task<IActionResult> GetBestNStoriesPaginated(
            [FromQuery] int n = 200,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var stories = await _newsService.GetTopNBestStoriesAsync(n);
            var totalStories = stories.Count;
            var pagedStories = stories
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            var response = new
            {
                Page = page,
                PageSize = pageSize,
                TotalStories = totalStories,
                Data = pagedStories
            };

            return Ok(response);
        }

        [HttpGet("top-stories")]
        public async Task<IActionResult> GetTopStories()
        {
            var storysIds = await _newsService.GetTopStoriesAsync();
            return Ok(storysIds);
        }

        [HttpGet("story/{id}")]
        public async Task<IActionResult> GetStory(int id)
        {
            var story = await _newsService.GetStoryByIdAsync(id);
            return story != null ? Ok(story) : NotFound();
        }

    }
}
