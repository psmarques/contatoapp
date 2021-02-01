using CadContato.WebApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CadContato.WebApi.Util
{
    public static class ExceptionMiddlewareExtensions
    {
        private const string File = "log.txt";
        private static void WriteInLogFile(IExceptionHandlerFeature obj)
        {
            try
            {
                if (obj == null || obj.Error == null) return;
                var ex = obj.Error;

                do
                {
                    WriteException(ex);
                    ex = ex.InnerException;
                }
                while (ex != null);
            }
            catch (System.Exception)
            {
            }
        }

        private static void WriteException(System.Exception obj)
        {
            System.IO.File.AppendAllText(File, string.Concat(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), System.Environment.NewLine));
            System.IO.File.AppendAllText(File, string.Concat(obj.Message, System.Environment.NewLine));
            System.IO.File.AppendAllText(File, string.Concat(obj.StackTrace, System.Environment.NewLine, System.Environment.NewLine));
        }

        public static void ConfigureExceptionHanlder(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "Application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    WriteInLogFile(contextFeature);

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new GenericErrorDTO(500, "Um Erro Ocorreu").ToString());
                    }
                });
            });
        }


    }
}
