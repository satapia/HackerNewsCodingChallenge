using HackerNewsStoriesAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsStoriesAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class HackerNewsController : Controller
{
    private readonly ILogger<HackerNewsController> _logger;
    private readonly IApiClient _service;

    public HackerNewsController(ILogger<HackerNewsController> logger, IApiClient service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "GetNewStories")]
    public async Task<IActionResult> GetAllStories()
    {
        var result = await _service.GetStories();
        return Ok(result);
    }
}
