using System;
using System.Threading.Tasks;
using Amazon.AppConfig.Model;
using AppConfigNETCoreSample.Helpers;
using AppConfigNETCoreSample.Models;
using Newtonsoft.Json;

namespace AppConfigNETCoreSample.Services {
  public class AppConfigDataService : IAppConfigDataService {
    private readonly Guid _clientId;

    public AppConfigDataService(Guid clientId) {
      _clientId = clientId;
    }

    public async Task<AppConfigData> GetAppConfigData() {
      // In general, we should limit the calls to GetConfiguration API call to at least once every 15 seconds
      // In AppConstants the TimeToLiveExpiration is set to the initial DateTime of Program.cs execution plus AppConstants.TimeToLiveInSeconds (15 seconds)
      // This if condition makes sure that we only call the GetConfiguration API call if we have not exceeded the TTL expiration, or
      // if the ClientConfigurationVersion is set in our local cache in AppConstants - ClientConfigurationVersion is returned from the initial call to the GetConfiguration API (see below for more info)
      if (DateTime.UtcNow > AppConstants.TimeToLiveExpiration || String.IsNullOrEmpty(AppConstants.ClientConfigurationVersion)) {
        Console.WriteLine("CALLED GetConfigurationAPI to get AppConfigData  \n");
        IAppConfigService appConfigService = new AppConfigService(_clientId);

        // get Amazon.AppConfig.Model.GetConfigurationResponse from GetConfiguration API Call
        GetConfigurationResponse getConfigurationResponse = await appConfigService.GetConfigurationResponse();


        // add ConfigurationVersion to AppConstants to AppConstants to be used in subsequent calls to GetConfugration API to avopid excess charges
        // https://docs.aws.amazon.com/appconfig/2019-10-09/APIReference/API_GetConfiguration.html#API_GetConfiguration_RequestSyntax
        AppConstants.ClientConfigurationVersion = getConfigurationResponse.ConfigurationVersion;


        // The GetConfiguration response includes a Content section (i.e., our getConfigurationResponse.Content) that shows the configuration data.
        // The Content section only appears if the system finds new or updated configuration data.
        // If the system doesn't find new or updated configuration data, then the Content section is not returned (Null).
        // https://docs.aws.amazon.com/appconfig/latest/userguide/appconfig-retrieving-the-configuration.html
        string decodedResponseData = getConfigurationResponse.Content.Length > 0 ? MemoryStreamHelper.DecodeMemoryStreamToString(getConfigurationResponse.Content) : String.Empty;


        // convert DecodedResponseData to our AppConfigData model which consists of:
        // bool boolEnableLimitResults
        // int intResultLimit
        AppConfigData appConfigData = this.ConvertDecodedResponseToAppConfigData(decodedResponseData);

        // add AppConfigData to our cache in AppConstants
        AppConstants.AppConfigData = appConfigData;

        return AppConstants.AppConfigData;
      } else {
        Console.WriteLine("DID NOT call GetConfigurationAPI to get data.  Return AppConfigData from cached value in AppConstants.AppConfigData instead. \n");
        return AppConstants.AppConfigData;
      }
    }

    private AppConfigData ConvertDecodedResponseToAppConfigData(string decodedResponseData) {
      // if the decodedResponseData is null we will return our cached AppConfigData stored in AppConstants
      // otherwise, we will deserialize the decodedResponseData into an AppConfigData object
      return String.IsNullOrEmpty(decodedResponseData) ? AppConstants.AppConfigData : JsonConvert.DeserializeObject<AppConfigData>(decodedResponseData);
    }
  }
}
