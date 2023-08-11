using System;
using Amazon.Extensions.NETCore.Setup;
using AppConfigNETCoreSample.Services;
using Microsoft.Extensions.Configuration;

namespace AppConfigNETCoreSample {
  class Program {

    /// <summary>
    /// AWSOptions instance with the configured "named profile" and "region", to be used when creating service clients
    /// </summary>
    public static AWSOptions AwsOptions { get; private set; }

    static async System.Threading.Tasks.Task Main(string[] args) {
      // Read the configuration from appsettings.json
      var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      AwsOptions = builder.Build().GetAWSOptions();

      IAppConfigDataService appConfigDataService = new AppConfigDataService();

      AppConstants.TimeToLiveExpiration = DateTime.UtcNow.AddSeconds(AppConstants.TimeToLiveInSeconds);

      Console.WriteLine("--------- Results from first execution ------- \n");
      var result = await appConfigDataService.GetAppConfigData();
      Console.WriteLine("boolEnableLimitResults: {0} \n", result.boolEnableLimitResults);
      Console.WriteLine("intResultLimit: {0} \n", result.intResultLimit);
      Console.WriteLine("\n");

      
      Console.WriteLine("--------- Program completed... Press any key to exit. ------- \n");
      Console.ReadKey(true);

    }
  }
}
