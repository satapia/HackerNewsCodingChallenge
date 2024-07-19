using HackerNewsApp.Server.Models;

namespace HackerNewsApp.Server.Service;
public interface IApiClient
{
    Task<List<StoryDto>> GetStories();
}
