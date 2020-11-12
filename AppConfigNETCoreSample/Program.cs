using System;
using System.Threading.Tasks;
using AppConfigNETCoreSample.Services;

namespace AppConfigNETCoreSample {
  class Program {
    static async System.Threading.Tasks.Task Main(string[] args) {
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

      Console.WriteLine("--------- Results from second execution after waiting another 20 secs ------- \n");
      result = await appConfigDataService.GetAppConfigData();
      Console.WriteLine("boolEnableLimitResults: {0} \n", result.boolEnableLimitResults);
      Console.WriteLine("intResultLimit: {0} \n", result.intResultLimit);
      Console.WriteLine("\n");

      Console.ReadLine();

    }
  }
}
