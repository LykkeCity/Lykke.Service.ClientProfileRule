using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Service.ClientProfileRule.Core.Exceptions;
using Lykke.Service.ClientProfileRule.Models;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Lykke.Service.ClientProfileRule.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly ILog _log;
        private readonly string _componentName;
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next, ILog log, string componentName)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _componentName = componentName ?? throw new ArgumentNullException(nameof(componentName));
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (RegulationRuleNotFoundException exception)
            {
                await LogError(context, exception);
                await CreateErrorResponse(context, exception.Message, (int)HttpStatusCode.NotFound);
            }
            catch (RegulationRuleAlreadyExistsException exception)
            {
                await LogError(context, exception);
                await CreateErrorResponse(context, exception.Message, (int)HttpStatusCode.Conflict);
            }
        }

        private async Task LogError(HttpContext context, Exception exception)
        {
            using (var ms = new MemoryStream())
            {
                context.Request.Body.CopyTo(ms);

                ms.Seek(0, SeekOrigin.Begin);

                await _log.LogPartFromStream(ms, _componentName, context.Request.GetUri().AbsoluteUri, exception);
            }
        }

        private async Task CreateErrorResponse(HttpContext ctx, string message, int status)
        {
            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = status;
            
            var json = JsonConvert.SerializeObject(ErrorResponse.Create(message));

            await ctx.Response.WriteAsync(json);
        }
    }
}
