using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToTheMoon.Api.Interfaces;
using ToTheMoon.Api.Service;
using Polly;
using Polly.Extensions.Http;

namespace ToTheMoon.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRouting();
            services.AddDistributedMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<CointreeHttpClient>()
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(2))
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromMinutes(5)));
            services.AddMemoryCache();
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(15));
            services.AddScoped<IChangePreferredCoinService, PreferredCoinService>();
            services.AddScoped<ICoinPriceService, CoinPriceService>();
            services.AddControllers();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>  {
                c.AllowAnyOrigin();
                c.AllowAnyMethod();
                c.AllowAnyHeader();
            });
            app.UseSession();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
