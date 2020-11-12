using System;
using System.Threading.Tasks;
using Amazon.AppConfig;
using Amazon.AppConfig.Model;

namespace AppConfigNETCoreSample.Services {
  public class AppConfigService : IAppConfigService {
    private readonly Guid _clientId;

    public AppConfigService(Guid clientId) {
      _clientId = clientId;
    }


    // returns Amazon.AppConfig.Model.GetConfigurationResponse which includes:
    // string ConfigurationVersion
    // MemoryStream Content (Base64 encoded memory stream)
    public async Task<GetConfigurationResponse> GetConfigurationResponse() {
      // creates AmazonAppConfigClient using AWS profile stored in app.config
      var amazonAppConfigClient = new AmazonAppConfigClient();
      // creates AmazonAppConfigClient using AWS programmatic access key and secret string
      //var amazonAppConfigClient = new AmazonAppConfigClient("YOUR ACCESS KEY", "YOUR SECRET STRING");

      var request = new GetConfigurationRequest {
        Application = AppConstants.AppConfigApplication,
        Environment = AppConstants.AppConfigEnvironment,
        Configuration = AppConstants.AppConfigConfiguration,
        ClientId = _clientId.ToString(),
        ClientConfigurationVersion = AppConstants.ClientConfigurationVersion
      };

      return await amazonAppConfigClient.GetConfigurationAsync(request);
    }
  }
}
