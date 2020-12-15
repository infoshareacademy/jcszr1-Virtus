using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace VirtusFitWeb.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            Console.WriteLine("Filter works");

            Log.Error("Error");

            base.OnException(context);
        }
    }
}