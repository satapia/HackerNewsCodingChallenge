using HackerNewsApp.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApp.Server.Controllers;


[ApiController]
[Route("[controller]")]
public class HackerNewsController : ControllerBase
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

    [HttpGet(Name = "Get")]
    public IActionResult Get()
    {
        return Ok(new string[] {"1", "2", "3"});
    }
}
