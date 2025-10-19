using CleanShortener.Data;
using CleanShortener.Application;

namespace CleanShortener
{
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

            // services
            builder.Services.AddSingleton<IUrlShortenerHandler, UrlShortenerHandler>();

            // infrastructure
            builder
                .Services
                .AddSingleton<IShortenedUrlDataProxy, ShortenedUrlDataProxy>()
                .AddSingleton<IShortenedUrlRepository, ShortenedUrlRepository>();

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
