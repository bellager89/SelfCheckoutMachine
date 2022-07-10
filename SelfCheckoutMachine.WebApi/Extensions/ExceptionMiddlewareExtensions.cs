using Microsoft.AspNetCore.Diagnostics;
using SelfCheckoutMachine.BusinessLogic;
using SelfCheckoutMachine.Models;
using System.Net;

namespace SelfCheckoutMachine.WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is UserException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetailModel
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message ?? "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}
