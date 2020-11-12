using System.Threading.Tasks;
using AppConfigNETCoreSample.Models;

namespace AppConfigNETCoreSample.Services {
  public interface IAppConfigDataService {
    Task<AppConfigData> GetAppConfigData();
  }
}
