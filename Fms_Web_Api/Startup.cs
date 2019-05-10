using Fms_Web_Api.Data.Interfaces;
using Fms_Web_Api.Data.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Fms_Web_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<FmsDbContext>();
            services.AddTransient<IMatchQuery, MatchQuery>();
            services.AddTransient<IGameDetailsQuery, GameDetailsQuery>();
            services.AddTransient<INewsQuery, NewsQuery>();
            services.AddTransient<IPlayerQuery, PlayerQuery>();
            services.AddTransient<IPlayerAttributeQuery, PlayerAttributeQuery>();
            services.AddTransient<IPlayerStatsQuery, PlayerStatsQuery>();
            services.AddTransient<ISeasonQuery, SeasonQuery>();
            services.AddTransient<ITeamQuery, TeamQuery>();
            services.AddTransient<ITeamSeasonQuery, TeamSeasonQuery>();
            services.AddTransient<IPlayerCreator, PlayerCreator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            //app.UseMvc(routes =>
            //    {
            //        routes
            //            .MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            //        //.MapRoute(name: "api", template: "api/{controller}/{gameDetailsId?}/{seasonId?}/{divisionId?}/{week?}");
            //    });
        }
    
    }
}
