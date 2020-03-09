using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPoolingEf;
using CarPoolingEf.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using CarPoolingEf.Services.Services;
using CarPoolingEf.Services.Interfaces;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarPoolWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarPoolingContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:CarPoolWebApiDb"]));
            services.AddScoped<IUserServices<User>, UserServices>();
            services.AddScoped<IRideServices, RideServices>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICarServices, CarServices>();
            services.AddControllers();
        }

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
