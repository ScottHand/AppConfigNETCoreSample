using System;
using Amazon.AppConfig.Model;
using AppConfigNETCoreSample.Models;

namespace AppConfigNETCoreSample {
  public static class AppConstants {
    public const string AppConfigApplication = "AppConfigLab";
    public const string AppConfigEnvironment = "AppConfigLabAPIGatewayDevelopment";
    public const string AppConfigConfiguration = "AppConfigLabHostedConfigurationProfile";
    public static string ClientConfigurationVersion;
    public static DateTime TimeToLiveExpiration;
    public static double TimeToLiveInSeconds = 15;
    public static AppConfigData AppConfigData;
    public static GetConfigurationResponse GetConfigurationResponse;
  }
}
