using HackerNewsApp.Server.Models;

namespace HackerNewsApp.Server.Common;

public interface ICacheManager
{
    List<StoryDto> GetNewObjects(List<StoryDto> inputList);
    public StoryDto GetObject(int item);
    public void AddObject(StoryDto item);
    public List<StoryDto> GetList();

}
