using Identity.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        Microsoft.Extensions.Primitives.StringValues _Token = "";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            //context.Result = new BadRequestObjectResult(new User());

            context.HttpContext.Request.Headers.TryGetValue("Token", out _Token);
            if (_Token.Count > 0)
            {
                string Token = _Token.FirstOrDefault();
                JwtRepo jwtRepo = new JwtRepo();
                Jwt jwt = jwtRepo.Get(Token);
                if (jwt == null)
                {
                    CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.BadRequest, null, "Token geçersiz.");
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                }

                var controller = context.Controller as Controller;
                controller.ViewBag.Jwt = jwt;

            }
            else
            {
                CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.BadRequest, null, "Header Token bulunamadı.");
                BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                context.Result = badReq;
            }
        }
    }
}
