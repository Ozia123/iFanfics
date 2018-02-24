using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using AutoMapper;
using iFanfics.Web.Util;
using Microsoft.AspNetCore.SpaServices.Webpack;
using iFanfics.DAL.Interfaces;
using iFanfics.DAL.Repositories;
using iFanfics.BLL.Interfaces;
using iFanfics.BLL.Services;

namespace iFanfics.Web {
    public class Startup {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            MapperConfiguration configMapper = new MapperConfiguration(
                cfg => { cfg.AddProfile(new AutoMapperProfile()); }
            );
            AutomapperConfiguration.Configure();

            services.AddMvc();

            services.AddScoped<IUnitOfWork, IdentityUnitOfWork>();
            services.AddScoped<IFanficService, FanficService>();
            services.AddScoped<IFanficTagsService, FanficTagsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChapterService, ChapterService>();
            services.AddScoped<IChapterRatingService, ChapterRatingService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRatingService, CommentRatingService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ITagService, TagService>();
            //services.AddScoped<IElasticService, ElasticService>();
            //services.AddScoped<IElasticRepository, ElasticRepository>();
            services.AddSingleton(ctx => configMapper.CreateMapper());

            var connection = @"Server=(localdb)\mssqllocaldb;Database=iFanfics.Db;Trusted_Connection=True;ConnectRetryCount=0;MultipleActiveResultSets=True";
            services.AddDbContext<ApplicationContext>(options => { options.UseSqlServer(connection); options.UseSqlServer(connection, b => b.MigrationsAssembly("iFanfics.Web")); });

            services.AddIdentity<ApplicationUser, IdentityRole>(opts => { opts.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IUserService userService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //userService.SeedDatabse().GetAwaiter().GetResult();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
