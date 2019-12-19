using Identity.Dapper;
using Identity.Dapper.Entities;
using Identity.Dapper.Models;
using Identity.Dapper.SqlServer.Connections;
using Identity.Dapper.SqlServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokeQuizWebAPI.AccountService;
using PokeQuizWebAPI.CalculationsService;
using PokeQuizWebAPI.PlayerServices;
using PokeQuizWebAPI.PokemonApiCall;
using PokeQuizWebAPI.PokemonDAL;
using PokeQuizWebAPI.PokemonServices;
using System.IO;

namespace PokeQuizWebAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", false, true)
           .AddEnvironmentVariables()
           .Build();
            var appConfig = new PokemonConfig();

            config.Bind("PokemonConfig", appConfig);
            services.AddSingleton(appConfig);

            var identityConnectionString = Configuration.GetSection("DapperIdentity");

            services.ConfigureDapperConnectionProvider<SqlServerConnectionProvider>(identityConnectionString)
                .ConfigureDapperIdentityCryptography(Configuration.GetSection("DapperIdentityCryptography"))
                .ConfigureDapperIdentityOptions(new DapperIdentityOptions { UseTransactionalBehavior = false });

            services.AddIdentity<DapperIdentityUser, DapperIdentityRole>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequiredLength = 1;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireUppercase = false;
            })
                   //.AddDapperIdentityFor<PostgreSqlConfiguration>()
                   .AddDapperIdentityFor<SqlServerConfiguration>()
                   //.AddDapperIdentityFor<MySqlConfiguration>()
                   .AddDefaultTokenProviders();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddSession(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddSingleton<IPokemonService, PokemonService>();
            services.AddSingleton<IPokemonApi, PokemonApi>();
            services.AddSingleton<IRandomizer, Randomizer>();
            services.AddSingleton<IQuizFlow, QuizFlow>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPokemonUserSQLService, PokemonUserSQLService>();
            services.AddSingleton<IPokemonUserSQLStore, PokemonUserSQLStore>();
            services.AddSingleton<IQuizCalculations, QuizCalculations>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          //  if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
            //}
            //else
            //{
             //   app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
              //  app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
