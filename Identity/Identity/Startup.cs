using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Identity.Filters;
using Identity.Middleware;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Identity
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
           
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Api versiyonlama için buradaki makala örnek alındı http://www.intstrings.com/ramivemula/articles/asp-net-core-web-api-versioning/
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });


            //Policy tanımlamasını yaptığımız kısımdır.
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("*")//AllowOrigins diyerek herkese ya da belirli adreslere erişim hakkı verebilirsiniz.
                   .AllowAnyMethod()
                   .AllowAnyHeader();
            }));

            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc(options => options.MaxModelValidationErrors = 50)
                 .AddJsonOptions(option =>
                 {   //Dönen Json'ı camelCase olarak formatlar.
                     option.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                 }); 

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("CorsPolicy");//CorsPolicy ismi ile tanımlanan güvenlik tanımlamasını tüm uygulama için geçerli olmasını sağlamış oluyoruz.
            app.UseMvc();
            app.UseResponseWrapper();

        }
    }
}
