using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;
using congestion.calculator.Rule;
using congestion.calculator.TollFee;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TaxCalculator.Api.Configuration;

namespace TaxCalculator.Api
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
            // Get configuration from config file
            // We should've used Options pattern for this purpose, instead we are doing it this way
            // for simplicity sake.
            var taxConfig = new TaxConfiguration();
            Configuration.Bind("TaxConfiguration", taxConfig);

            // Initialize rules with configuration values
            // Please note that we could initialize toll fees through configuration same way as we did for dates
            services.AddTransient<ICompareDateRules, CompareDateRules>(sp =>
                new CompareDateRules(new List<Predicate<DateTime>>
                {
                    x => taxConfig
                        .GetParsedHolidays()
                        .Any(h => h.Date == x.Date || h.AddDays(-1).Date == x.Date),
                    x => taxConfig.OnMonths.Any(m => x.Month == m),
                    x => taxConfig.GetSpecificDays().Any(dof => dof == x.DayOfWeek)
                }));

            services.AddTransient<ITollFeeProvider, TollFeeProvider>();
            services.AddTransient<ITollFreeVehicleProvider, TollFreeVehicleProvider>();

            // Register TaxCalculator with dependencies.
            services.AddTransient<ITaxCalculator, CongestionTaxCalculator>(sp =>
                new CongestionTaxCalculator(sp.GetRequiredService<ICompareDateRules>(),
                    sp.GetRequiredService<ITollFeeProvider>(),
                    sp.GetRequiredService<ITollFreeVehicleProvider>(),
                    taxConfig.MaxTaxPerDay,
                    taxConfig.SingleChargeRuleInSeconds));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "TaxCalculator.Api", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaxCalculator.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}