using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using AutoMapper;

using FluentValidation;
using FluentValidation.AspNetCore;

using MediatR;

using SampleMVCWithCQS2.Application.Queries;
using SampleMVCWithCQS2.Application.Validators;

using SampleMVCWithCQS2Core.DataAccess;

using SampleMVCWithCQS2Core.Domain;


namespace SampleMVCWithCQS2
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddMvc().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });
#if DEBUG
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"],
                // sqlServerOptionsAction: sqlOptions =>
                // {
                //     sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                //     sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                // });
                options.UseSqlite(Configuration["ConnectionStrings:SqliteConnection"],
                sqliteOptionsAction: sqlOptions =>
                   {
                       sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                   });
            },
            ServiceLifetime.Scoped //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );
            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatorHandlers(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<IProductQueries>(s => new ProductQueries(Configuration["ConnectionStrings:SqliteConnection"]));
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedData.Seed(dbContext);

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Products}");
            });
        }
    }
}