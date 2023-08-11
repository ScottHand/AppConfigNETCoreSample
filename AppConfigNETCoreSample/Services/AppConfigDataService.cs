using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amazon.AppConfig.Model;
using AppConfigNETCoreSample.Helpers;
using AppConfigNETCoreSample.Models;
using Newtonsoft.Json;

namespace AppConfigNETCoreSample.Services {
  public class AppConfigDataService : IAppConfigDataService {
    public async Task<AppConfigData> GetAppConfigData() {
      // Call the API Gateway endpoint that is backed by the Lambda 
      var path = AppConstants.AppConfilgApiGatewayEnpointUrl;
      AppConfigData appConfigData = null;
      HttpClient client = new HttpClient();
      HttpResponseMessage response = await client.GetAsync(path);

      if (response.IsSuccessStatusCode) {
        var result = await response.Content.ReadAsStringAsync();
        appConfigData = ConvertDecodedResponseToAppConfigData(result);
      } else {
        Console.WriteLine("error");
      }
      return appConfigData;
    }

    private AppConfigData ConvertDecodedResponseToAppConfigData(string decodedResponseData) {
      // if the decodedResponseData is null we will return our cached AppConfigData stored in AppConstants
      // otherwise, we will deserialize the decodedResponseData into an AppConfigData object
      return String.IsNullOrEmpty(decodedResponseData) ? AppConstants.AppConfigData : JsonConvert.DeserializeObject<AppConfigData>(decodedResponseData);
    }
  }
}
