using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Orderly.RestaurantWeb
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args)
    {
      var hostParam = args.SingleOrDefault(x => x.StartsWith("--host", StringComparison.OrdinalIgnoreCase));
      var portParam = args.SingleOrDefault(x => x.StartsWith("--port", StringComparison.OrdinalIgnoreCase));

      var hostParamValue = hostParam?.Replace("--host", string.Empty).Trim(' ', ':', '=');
      var portParamValue = portParam?.Replace("--port", string.Empty).Trim(' ', ':', '=');

      var useUrl = !string.IsNullOrWhiteSpace(hostParamValue) && !string.IsNullOrWhiteSpace(portParamValue)
                     ? $"http://{hostParamValue}:{portParamValue}/"
                     : "http://localhost:56521/";


      return WebHost.CreateDefaultBuilder(args)
                    .UseUrls(useUrl)
                    .UseStartup<Startup>()
                    .Build();
    }
  }
}
