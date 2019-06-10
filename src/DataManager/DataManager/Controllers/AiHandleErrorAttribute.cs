using Microsoft.ApplicationInsights;
using System;
using System.Web.Mvc;

namespace DataManager.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AiHandleErrorAttribute : HandleErrorAttribute
    {
        private static readonly TelemetryClient Telemetry = new TelemetryClient();

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.HttpContext != null && filterContext.Exception != null)
            {
                //If customError is Off, then AI HTTPModule will report the exception
                if (filterContext.HttpContext.IsCustomErrorEnabled)
                {
                    Telemetry.TrackException(filterContext.Exception);
                }
            }
            base.OnException(filterContext);
        }
    }
}