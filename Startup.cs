using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartStocksAPI.Controllers;
using SmartStocksAPI.Data;

namespace SmartStocksAPI
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
            services.AddDbContext<WalletContext>(opt => opt.UseInMemoryDatabase("WalletList"));            
            services.AddDbContext<FundContext>(opt => opt.UseInMemoryDatabase("FundList"));
            services.AddDbContext<RecommendedWalletContext>(opt => opt.UseInMemoryDatabase("RecommendedWalletList"));

            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddScoped<IWalletController, WalletController>();
            services.AddScoped<IFundController, FundController>();

            services.AddMvc().AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
