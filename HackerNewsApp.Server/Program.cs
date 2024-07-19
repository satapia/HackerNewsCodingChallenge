
using HackerNewsApp.Server.Common;
using HackerNewsApp.Server.Service;
using Microsoft.Extensions.Options;

namespace HackerNewsApp.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMemoryCache();
        builder.Services.AddHttpClient<IApiClient, ApiClient>(c => c.BaseAddress = new Uri(builder.Configuration["BaseUrl"]));
        builder.Services.AddSingleton<ICacheManager, CacheManager>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("PermitirTodo",
            builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            });
        });
        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.UseCors("PermitirTodo");

        app.Run();
    }
}
