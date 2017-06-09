using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Identity.Middleware
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                //set the current response to the memorystream.
                context.Response.Body = memoryStream;

                await _next(context);

                //reset the body 
                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                var objResult = JsonConvert.DeserializeObject(readToEnd);
                var result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, false, objResult, "");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }

    }

    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapper>();
        }
    }


    public class CommonApiResponse
    {
        public bool Status = false;
        public string Version => "1.0";
        public int StatusCode { get; set; }
        public string RequestId { get; }
        public List<ValidationFailure> ErrorMessage { get; set; }
        public object Result { get; set; }

        public static CommonApiResponse Create(HttpStatusCode statusCode, bool Status, object result, object errorMessage)
        {
            List<ValidationFailure> errList = new List<ValidationFailure>();
            ValidationFailure err = new ValidationFailure("",errorMessage.ToString());
            errList.Add(err);
        
            return new CommonApiResponse(statusCode, Status, result, errList);
        }

        public static CommonApiResponse Create(HttpStatusCode statusCode, bool Status, object result, List<ValidationFailure> errorMessage)
        {
            return new CommonApiResponse(statusCode, Status, result, errorMessage);
        }

        public static CommonApiResponse Create(HttpResponse Response, HttpStatusCode statusCode, bool Status, object result, List<ValidationFailure> errorMessage)
        {
            return new CommonApiResponse(Response, statusCode, Status, result, errorMessage);
        }


       protected CommonApiResponse(HttpStatusCode statusCode, bool status, object result, List<ValidationFailure> errorMessage)
       {
           RequestId = Guid.NewGuid().ToString();
           StatusCode = (int)statusCode;
           Result = result;
           ErrorMessage = errorMessage;
           Status = status;
       }

        protected CommonApiResponse(HttpResponse Response, HttpStatusCode statusCode, bool status, object result, List<ValidationFailure> errorMessage)
        {
            RequestId = Guid.NewGuid().ToString();
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
            Status = status;
            Response.StatusCode = (int)statusCode;
        }

    }
}
