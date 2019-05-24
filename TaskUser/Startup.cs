using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskUser.Models;
using TaskUser.Resources;
using TaskUser.Service;
using TaskUser.Validator;

namespace TaskUser
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
// Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(600);
                options.Cookie.HttpOnly = true;
// Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            #region snippet1
            
            services.AddLocalization(options => options.ResourcesPath = "Resources");         
            services.AddMvc()
                .AddViewLocalization(opts => { opts.ResourcesPath = "Resources";})
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(
//                    options =>
//                {
//                    options.DataAnnotationLocalizerProvider = (type, factory) =>
//                    {
//                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
//                        return factory.Create("SharedResource", assemblyName.Name);
//                    };
//                }
                );
         
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    
                    new CultureInfo("en-US"),
                    new CultureInfo("vi-VN"),
                  
                    
                    
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });
//            services.Configure<IdentityOptions>(options =>
//            {
//                // Users settings
//                options.Users.RequireUniqueEmail = true;
//            });

            #endregion
            services.AddAutoMapper();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IStockService, StockService>();
            services.AddSingleton<SharedViewLocalizer<CategoryResource>>();
            services.AddSingleton<SharedViewLocalizer<BrandResource>>();
            services.AddSingleton<SharedViewLocalizer<ProductResource>>();
            services.AddSingleton<SharedViewLocalizer<LoginResource>>();
            services.AddSingleton<SharedViewLocalizer<StockResource>>();
            services.AddSingleton<SharedViewLocalizer<CommonResource>>();
            services.AddSingleton<SharedViewLocalizer<StoreResource>>();
            services.AddSingleton<SharedViewLocalizer<UserResource>>();
            services.AddSingleton<SharedViewLocalizer<PasswordResource>>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                    {
                        options.AccessDeniedPath = new PathString("/Error/401");
                        options.LoginPath = new PathString("/Login/LoginIndex");
                        options.ReturnUrlParameter = "RequestPath";
                        options.SlidingExpiration = true;
                    }
                );
            services.AddTransient<DbContext>();
           
//            services.AddScoped<ActionFilter>();
            services.AddHttpContextAccessor();
            services.AddDbContext<DataContext>(item => item.UseSqlServer(Configuration.GetConnectionString("userscontext")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BrandValidator>()
                );
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            
            app.UseAuthentication();
            app.Use(async (context, next) =>
            {
                await next();
                if (!context.Response.HasStarted && context.Response.StatusCode != StatusCodes.Status200OK)
                {
                  

                     if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                    {
                        context.Request.Path = "/Error/401";
                        await next();
                    }
                    else if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                     {
                         context.Request.Path = "/Error/404";
                         await next();
                     }
                        
                } 
            });
            #region snippet2
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            #endregion
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=IndexLogin}/{id?}");
                 
            });
            app.UseCookiePolicy();   
            
        }
         
    }
}