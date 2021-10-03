using AutomatedFinances.Infrastructure.Data;
using AutomatedFinances.Infrastructure.Data.ExpendituresDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutomatedFinances.Infrastructure
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var s = new ServerSettings();

            RegisterAndConfigureDatabase(s);

            services.AddDbContext<ExpenditureReadDbContext>();
            services.AddDbContext<ExpenditureWriteDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }


        private static void RegisterAndConfigureDatabase(ServerSettings settings)
        {
            var dbSettings = new DatabaseSettings
            {
                Server = settings.SqlServerPath,
                Password = settings.SqlPassword
            };

            // add entity framework
            // register instance of db settings singleton
            // db context lifetime scope
            // register any converters here
            // add the db instance
        }
    }
}
