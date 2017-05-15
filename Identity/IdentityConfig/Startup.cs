using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IdentityConfig
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
          //  var configurationBuilder = new ConfigurationBuilder()
          // .SetBasePath(Directory.GetCurrentDirectory())
          // .AddJsonFile("greetings.json", optional: false, reloadOnChange: true);
          //
          //  Configuration = configurationBuilder.Build();
        }

       

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Setup logging
            // Configure app

        }

        public void ConfigureServices(IServiceCollection services)
        {
           // services.Add(new ServiceDescriptor(typeof(IConfiguration),
           //           provider => new ConfigurationBuilder()
           //                          .SetBasePath(Directory.GetCurrentDirectory())
           //                          .AddJsonFile("greetings.json",
           //                                       optional: false,
           //                                       reloadOnChange: true)
           //                          .Build(),
           //           ServiceLifetime.Singleton));
           //
           // services.AddSingleton<IGreetingService, GreeringService>();
        }
    }
}
