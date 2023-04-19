using HackerNewsStoriesAPI;
using HackerNewsStoriesAPI.Common;
using Microsoft.Extensions.Caching.Memory;


public class CacheManager : ICacheManager 
{ 
    private readonly IMemoryCache cache;
    private readonly IConfiguration _configuration;
    private readonly int _maxRecords;

    public CacheManager(IMemoryCache cache,IConfiguration config)
    {
        this.cache = cache;
        this._configuration = config;
        _maxRecords = Convert.ToInt32(_configuration.GetValue<string>("MaxRecords"));
    }

    public List<StoryDto> GetNewObjects(List<StoryDto> inputList)
    {
        var cacheKey = typeof(StoryDto).Name + "CacheKey";
        var cachedList = cache.Get<List<StoryDto>>(cacheKey);

        if (cachedList == null)
        {
            cachedList = new List<StoryDto>();
            cache.Set(cacheKey, cachedList);
        }

        var newObjects = inputList.Where(i => !cachedList.Any(c => c.id == i.id)).ToList();
        cachedList.AddRange(newObjects);
        cache.Set(cacheKey, cachedList);
        return newObjects;
    }

    public StoryDto GetObject(int id)
    {
        var cacheKey = typeof(StoryDto).Name + "CacheKey";
        var cachedList = cache.Get<List<StoryDto>>(cacheKey);

        if (cachedList == null)
        {
            cachedList = new List<StoryDto>();
            cache.Set(cacheKey, cachedList);
        }

       return cachedList.FirstOrDefault(c => c.id == id);
    }

    public void AddObject(StoryDto item)
    {
        var cacheKey = typeof(StoryDto).Name + "CacheKey";
        var cachedList = cache.Get<List<StoryDto>>(cacheKey);

        if (cachedList == null)
        {
            cachedList = new List<StoryDto>();
            cache.Set(cacheKey, cachedList);
        }

        cachedList.Add(item);
        cache.Set(cacheKey, cachedList);
    }

    public List<StoryDto> GetList()
    {
        var cacheKey = typeof(StoryDto).Name + "CacheKey";
        var cachedList = cache.Get<List<StoryDto>>(cacheKey);

        var res = cachedList?.OrderByDescending(x => x.time).Take(_maxRecords).ToList();
        return res;
    }
}