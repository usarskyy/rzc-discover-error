using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Orderly.RestaurantWeb.Services;

namespace Orderly.RestaurantWeb
{
  public class Startup
  {
    private static readonly NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

    private IHostingEnvironment HostingEnvironment { get; }
    public IConfiguration Configuration { get; }

    public Startup(IHostingEnvironment env)
    {
      _log.Info("Application started");

      try
      {
        HostingEnvironment = env;

        var builder = new ConfigurationBuilder()
                      .SetBasePath(env.ContentRootPath)
                      .AddJsonFile("appsettings.json")
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                      .AddEnvironmentVariables();

        Configuration = builder.Build();
      }
      catch (Exception ex)
      {
        _log.Error(ex);
      }
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
      services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


      services.AddTransient<IAppConfig, AppConfig>();
      
      services.Configure<RazorViewEngineOptions>(o => { o.ViewLocationFormats.Add("/{1}/Views/{0}" + RazorViewEngine.ViewExtension); });

      services.Configure<RequestLocalizationOptions>(
                                                     opts =>
                                                     {
                                                       opts.RequestCultureProviders.Clear();

                                                       opts.RequestCultureProviders.Add(new CookieRequestCultureProvider());
                                                       opts.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
                                                     });


      services.AddMvc()
              .AddDataAnnotationsLocalization();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      _log.Info("Is development environment: " + env.IsDevelopment());

      app.UseForwardedHeaders(new ForwardedHeadersOptions
                              {
                                ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedProto
                              });
      app.UseHttpMethodOverride();

      var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
      app.UseRequestLocalization(options.Value);

      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}