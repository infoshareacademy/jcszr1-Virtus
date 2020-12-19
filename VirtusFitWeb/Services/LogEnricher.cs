using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using System;

namespace VirtusFitWeb.Services
{
    public class LogEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var context = new HttpContextAccessor();

            string userName;

            if (context.HttpContext == null)
            {
                logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("User", null));
            }
            else
            {
                userName = context.HttpContext.User.Identity.Name;

                if (userName == null)
                {
                    logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("User", null));
                }
                else
                {
                    logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("User", userName));
                }
            }
        }
    }
}