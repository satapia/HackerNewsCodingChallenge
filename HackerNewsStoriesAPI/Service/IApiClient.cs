namespace HackerNewsStoriesAPI.Service;
public interface IApiClient
{
    Task<List<StoryDto>> GetStories();
}
