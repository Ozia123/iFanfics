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
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
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
            services.AddSingleton(Configuration);
            services.AddSingleton(ctx => configMapper.CreateMapper());

            System.IO.File.WriteAllText("D:/startup.txt", Configuration.GetConnectionString("ApplicationContext"));

            services.AddDbContext<ApplicationContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext"), b => b.MigrationsAssembly("iFanfics.Migrations"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(opts => { opts.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IUserService userService) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes => {
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
