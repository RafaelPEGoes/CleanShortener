using CleanShortener.Application;
using CleanShortener.Data;
using CleanShortener.Data.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;

namespace CleanShortener;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter the Token only."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddDbContext<CleanShortenerDbContext>();

        // identity (keep existing identity registration if needed)
        builder.Services
            .AddAuthorization()
            .AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<CleanShortenerDbContext>();

        // services
        builder.Services.AddScoped<IUrlShortenerHandler, UrlShortenerHandler>();

        // infrastructure
        builder
            .Services
            .AddScoped<IShortenedUrlDataProxy, ShortenedUrlDataProxy>()
            .AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();

        builder.Services.AddMemoryCache();

        const string ServiceVersion = "1.0.0";

        builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resource =>
                resource.AddService(nameof(CleanShortener), serviceVersion: ServiceVersion))
            .WithMetrics()
            .WithTracing();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanShortener API v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers().RequireAuthorization();

        app.MapIdentityApi<IdentityUser>();

        app.Run();
    }
}