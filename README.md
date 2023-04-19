# HackerNewsCodingChallenge

Using the Hacker News API, create a solution that allows users to view the newest stories from the feed.
Your solution must be developed using an Angular front-end app that calls a C# .NET Core back-end restful API.

## Endpoint

###https://hacker-news.firebaseio.com/v0/item/8863.json?print=pretty
{
  "by" : "dhouston",
  "descendants" : 71,
  "id" : 8863,
  "kids" : [ 8952, 9224, 8917, 8884, 8887, 8943, 8869, 8958, 9005, 9671, 8940, 9067, 8908, 9055, 8865, 8881, 8872, 8873, 8955, 10403, 8903, 8928, 9125, 8998, 8901, 8902,   8907, 8894, 8878, 8870, 8980, 8934, 8876 ],
  "score" : 111,
  "time" : 1175714200,
  "title" : "My YC app: Dropbox - Throw away your USB drive",
  "type" : "story",
  "url" : "http://www.getdropbox.com/u/2/screencast.html"
}

Up to 200 of the latest Ask HN, Show HN, and Job stories are at /v0/askstories, /v0/showstories, and /v0/jobstories.

###https://hacker-news.firebaseio.com/v0/askstories.json?print=pretty
[ 9127232, 9128437, 9130049, 9130144, 9130064, 9130028, 9129409, 9127243, 9128571, ..., 9120990 ]


###appsettings file should look like this!
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "BaseUrl": "",
  "MaxRecords": 200
}
