using Identity.Api.Service;
using Identity.Api.Service.Interfaces;
using Identity.Service.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Identity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<IdentityContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddSwaggerGen(options =>
                options.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "Identity API",
                    Version = "v3",
                    Description = "API for managing Users."
                })
            );


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();
            app.UseSwagger(c =>
                c.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0
            );
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("v3/swagger.json", "Identity API V1")
            );

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
