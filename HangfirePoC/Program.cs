using Hangfire;
using HangfirePoC.Dal.DbContexts;
using HangfirePoC.Services;
using HangfirePoC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace HangfirePoC
{
    /// <summary>
    /// Application main entry point.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        /// <remarks>Usually one would use something like the Options Pattern instead for this.</remarks>
        private const string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=HangfirePoc;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

        /// <summary>
        /// Application main entry point.
        /// </summary>
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            Configure(app);

            app.Run();
        }

        #region Startup

        private static void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);

            services.AddHangfire(config => config.UseSqlServerStorage(ConnectionString));
            services.AddHangfireServer();
            services.AddScoped<IHangfireJobs, HangfireJobs>();
            services.AddScoped<IHangfireService, HangfireService>();
        }

        private static void Configure(WebApplication app)
        {
            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(new SwaggerOptions());

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hangfire Proof of Concept API V1");
                    c.RoutePrefix = "swagger";
                });
            }

            // Enable HTTPS redirection for enhanced security.
            app.UseHttpsRedirection();

            // Enable authorization/authentication if needed.
            app.UseAuthorization();

            // Hangfire must be configured before the controllers and after the auth.
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                // If you want to apply authorization to Hangfire dashboard:
                // Authorization = new [] { new MyHangfireAuthorizationFilter() }
            });

            // Map controllers to routes.
#pragma warning disable ASP0014 // ASP0014 Suggest using top level route registrations instead of UseEndpoints.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
#pragma warning restore ASP0014
        }

        private static void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<HangfirePocDbContext>(options =>
                options.UseSqlServer(ConnectionString));
        }

        #endregion
    }
}