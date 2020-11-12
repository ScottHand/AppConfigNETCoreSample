using System.Threading.Tasks;
using Amazon.AppConfig.Model;

namespace AppConfigNETCoreSample.Services {
  public interface IAppConfigService {
    Task<GetConfigurationResponse> GetConfigurationResponse();
  }
}
