namespace Orderly.RestaurantWeb.Services
{
  public static class AppConfigExtensions
  {
    public static string GetApiEndpointUrl(this IAppConfig cfg, string endpointName)
    {
      var baseUrl = cfg.GetApiBaseUrl();

      if (endpointName[0] != '/' && baseUrl[baseUrl.Length - 1] != '/')
      {
        return $"{baseUrl}/{endpointName}";
      }

      if (endpointName[0] == '/' && baseUrl[baseUrl.Length - 1] == '/')
      {
        return $"{baseUrl}{endpointName.TrimStart('/')}";
      }

      return $"{baseUrl}{endpointName}";
    }
  }
}