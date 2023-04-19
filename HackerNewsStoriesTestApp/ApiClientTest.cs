using HackerNewsStoriesAPI;
using HackerNewsStoriesAPI.Common;
using HackerNewsStoriesAPI.Service;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HackerNewsStoriesTestApp
{
    public class ApiClienttTest
    {
        public class HttpMessageHandlerMock : HttpMessageHandler
        {
            private readonly HttpStatusCode _code;
            private readonly Mock<IConfiguration> _configurationMock;
            private readonly HttpResponseMessage _response;

            public HttpMessageHandlerMock(HttpStatusCode code)
            {
                _code = code;
                _configurationMock = new Mock<IConfiguration>();

                _configurationMock.Setup(x => x["MaxRecords"]).Returns("200");
            }

            public HttpMessageHandlerMock(HttpResponseMessage response)
            {
                _response = response;

                _configurationMock = new Mock<IConfiguration>();
                _configurationMock.Setup(x => x["MaxRecords"]).Returns("200");
    
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation)
            {
                if(_response != null)
                {
                    return Task.FromResult(_response);
                }
                return Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = _code
                }); 
            }
        }

        [Fact]
        public async Task GetStories_ReturnsNull_When_400()
        {
            var cacheMock = new Mock<ICacheManager>();

            var http = new HttpClient(new HttpMessageHandlerMock(HttpStatusCode.BadRequest));
            http.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");

            var service = new ApiClient(http, cacheMock.Object);
            var result = await service.GetStory(It.IsAny<string>());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetStories_ReturnsNull_When_200()
        {
            var story = new StoryDto() 
            { 
                id = 1,
                title = "Story 1",
                time =  ((int)DateTime.Now.Ticks)
            };
            

            var cacheMock = new Mock<ICacheManager>();

            var http = new HttpClient(new HttpMessageHandlerMock(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(story))

            }));

            http.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");

            var service = new ApiClient(http, cacheMock.Object);
            var result = await service.GetStory("1");

            Assert.NotNull(result);
            Assert.Equal(1, result.id);

        }
    }
}