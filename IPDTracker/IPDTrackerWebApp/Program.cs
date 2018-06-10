using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace IPDTrackerWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            //CreateWebHostBuilder(args).Build().Run();
        }

        public class PrefixKeyVaultSecretManager : IKeyVaultSecretManager
        {
            private readonly string _prefix;

            public PrefixKeyVaultSecretManager(string prefix)
            {
                _prefix = $"{prefix}-";
            }

            public bool Load(SecretItem secret)
            {
                // Load a vault secret when its secret name starts with the 
                // prefix. Other secrets won't be loaded.
                return secret.Identifier.Name.StartsWith(_prefix);
            }

            public string GetKey(SecretBundle secret)
            {
                // Remove the prefix from the secret name and replace two 
                // dashes in any name with the KeyDelimiter, which is the 
                // delimiter used in configuration (usually a colon). Azure 
                // Key Vault doesn't allow a colon in secret names.
                return secret.SecretIdentifier.Name
                    .Substring(_prefix.Length)
                    .Replace("--", ConfigurationPath.KeyDelimiter);
            }
        }

    //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //        WebHost.CreateDefaultBuilder(args)
    //    .ConfigureAppConfiguration((context, config) =>
    //    {
    //        var appVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
    //        var versionPrefix = appVersion.Replace(".", string.Empty);

    //        var builtConfig = config.Build();

    //        var keyVaultConfigBuilder = new ConfigurationBuilder();

    //        keyVaultConfigBuilder.AddAzureKeyVault
    //            (
    //            $"https://{builtConfig["Vault"]}.vault.azure.net/",
    //            builtConfig["ClientId"],
    //            builtConfig["ClientSecret"],
    //            new PrefixKeyVaultSecretManager(versionPrefix));

    //        var keyVaultConfig = keyVaultConfigBuilder.Build();

    //        config.AddConfiguration(keyVaultConfig);
    //    })
    //    .UseStartup<Startup>();
    //}

    public static IWebHost BuildWebHost(string[] args) =>
               WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .Build();

    }
}
