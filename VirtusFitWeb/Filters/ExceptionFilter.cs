using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;

namespace VirtusFitWeb.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Console.WriteLine("Filter works");


            Log.Fatal(context.Exception, $"Unhandled exception");

            base.OnException(context);
        }
    }
}