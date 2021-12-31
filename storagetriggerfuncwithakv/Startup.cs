using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(StorageTriggerFuncWithAkv.Startup))]

namespace StorageTriggerFuncWithAkv
{
    public class Startup : FunctionsStartup
    {
        public static IConfigurationRoot Configuration { get; set; }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var confbuilder = new ConfigurationBuilder().AddKeyPerFile("/mnt/secrets-store/connstring");
            Configuration = confbuilder.Build();
        }
    }
}