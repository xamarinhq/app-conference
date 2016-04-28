using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using XamarinEvolve.DataObjects;
using XamarinEvolve.Backend.Models;
using Owin;
using Microsoft.Azure.Mobile.Server;
using WebApiThrottle;

namespace XamarinEvolve.Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute("XamarinAuthProvider", ".auth/login/xamarin", new { controller = "XamarinAuth" });

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

                new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            
            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new XamarinEvolveContextInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            // This middleware is intended to be used locally for debugging. By default, HostName will
            // only have a value when running in an App Service application.
            if (string.IsNullOrEmpty(settings.HostName))
            {
                app.UseAppServiceAuthentication(new Microsoft.Azure.Mobile.Server.Authentication.AppServiceAuthenticationOptions
                {

                });
            }

           

            app.UseWebApi(config);
      
        }
    }
}

