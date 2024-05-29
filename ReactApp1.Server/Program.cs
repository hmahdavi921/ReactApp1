using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Areas.Identity.Data;
using ReactApp1.Server.Data;

namespace ReactApp1.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ReactApp1ServerContextConnection") ?? throw new InvalidOperationException("Connection string 'ReactApp1ServerContextConnection' not found.");

            builder.Services.AddDbContext<ReactApp1ServerContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ReactApp1ServerUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ReactApp1ServerContext>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}
