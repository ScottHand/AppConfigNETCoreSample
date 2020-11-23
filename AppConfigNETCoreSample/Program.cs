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

      Guid g = new Guid();

      IAppConfigDataService appConfigDataService = new AppConfigDataService(g);

      AppConstants.TimeToLiveExpiration = DateTime.UtcNow.AddSeconds(AppConstants.TimeToLiveInSeconds);

      Console.WriteLine("--------- Results from first execution ------- \n");
      var result = await appConfigDataService.GetAppConfigData();
      Console.WriteLine("boolEnableLimitResults: {0} \n", result.boolEnableLimitResults);
      Console.WriteLine("intResultLimit: {0} \n", result.intResultLimit);
      Console.WriteLine("\n");

      System.Threading.Thread.Sleep(10000);

      Console.WriteLine("--------- Results from second execution after waiting 10 secs ------- \n");
      result = await appConfigDataService.GetAppConfigData();
      Console.WriteLine("boolEnableLimitResults: {0} \n", result.boolEnableLimitResults);
      Console.WriteLine("intResultLimit: {0} \n", result.intResultLimit);
      Console.WriteLine("\n");

      System.Threading.Thread.Sleep(10000);

      Console.WriteLine("--------- Results from third execution after waiting another 10 secs ------- \n");
      result = await appConfigDataService.GetAppConfigData();
      Console.WriteLine("boolEnableLimitResults: {0} \n", result.boolEnableLimitResults);
      Console.WriteLine("intResultLimit: {0} \n", result.intResultLimit);
      Console.WriteLine("\n");

      Console.WriteLine("--------- Program completed... Press any key to exit. ------- \n");
      Console.ReadKey(true);

    }
  }
}
