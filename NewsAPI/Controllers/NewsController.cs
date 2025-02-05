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

        [HttpGet("best-n-stories")]
        public async Task<IActionResult> GetBestStories([FromQuery] int n)
        {
            var stories = await _newsService.GetTopNBestStoriesAsync(n);

            return Ok(stories);
        }

    }
}
