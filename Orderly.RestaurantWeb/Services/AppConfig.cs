using System;
using Microsoft.Extensions.Configuration;

namespace Orderly.RestaurantWeb.Services
{
  public class AppConfig : IAppConfig
  {
    private IConfiguration Cfg { get; }

    public AppConfig(IConfiguration cfg)
    {
      Cfg = cfg ?? throw new ArgumentNullException(nameof(cfg));
    }

    public string GetApiBaseUrl()
    {
      //return Cfg.GetValue<string>("Api:BaseUrl");
      return "http://localhost:56520/";
    }
  }
}