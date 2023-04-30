using cms_api.Data;
using cms_api.Middleware;
using cms_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace cms_api
{
    public class Program
    {
        public static void Main(string[] args)
        {

          
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // register EF to the builder with sql server
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/hyper"), builder =>
                builder.UseMiddleware<AuthMiddleware>()
            );

            app.UseCors(options =>
               options.WithOrigins("*")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
           );

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}