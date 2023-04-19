using HackerNewsStoriesAPI.Common;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text.RegularExpressions;

namespace HackerNewsStoriesAPI.Service;
public class ApiClient : IApiClient
{
    private HttpClient _client;
    private ICacheManager _cacheManager;

    public ApiClient(HttpClient httpClient, ICacheManager cacheManager)
    {
        _client = httpClient;
        _cacheManager = cacheManager;
    }
    public async Task<List<StoryDto>> GetStories()
    {
        var response = await _client.GetAsync("topstories.json");
        
        if (response.IsSuccessStatusCode)
        {
            string pattern = @"\[|\]";
            var result = await response.Content.ReadAsStringAsync();
            var output = Regex.Replace(result, pattern, "");

            var storiesIds = new List<string>( output.Split(","));

            var tasks = new List<Task<StoryDto>>();
            
            foreach (var story in storiesIds)
            {
                tasks.Add(Task.Run(async () => { return await GetStory(story); }));
            }

            var continuation = Task.WhenAll(tasks);
            try
            {
                continuation.Wait();
            }
            catch (AggregateException)
            { }

            if (continuation.Status == TaskStatus.RanToCompletion)
            {
                return _cacheManager.GetList();
            }
            else
            {
                foreach (var t in tasks)
                    Console.WriteLine("Task {0}: {1}", t.Id, t.Status);
            }

            return null;
        }
        else
        {
            return null;
        }
    }

    public async Task<StoryDto> GetStory(string id)
    {
        if (_cacheManager.GetObject(Convert.ToInt32(id)) == null)
        {
            var response = await _client.GetAsync($"item/{id}.json");
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadFromJsonAsync<StoryDto>();
                _cacheManager.AddObject(res);
                return res;
            }
        }
        return null;
    }

}
