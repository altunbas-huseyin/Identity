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
        JwtRepo jwtRepo = new JwtRepo();
        UserRepo userRepo = new UserRepo();
        string Role = "";
        public ValidateModelAttribute(string _role)
        {
            Role = _role;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Role = Role;

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            //context.Result = new BadRequestObjectResult(new User());

            context.HttpContext.Request.Headers.TryGetValue("Token", out _Token);
            if (_Token.Count > 0)
            {
                string Token = _Token.FirstOrDefault();

                Jwt jwt = jwtRepo.Get(Token);
                if (jwt == null)
                {
                    CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.BadRequest, null, "Token geçersiz.");
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                }

                var controller = context.Controller as Controller;


                User user = userRepo.GetById(jwt.UserId);
                controller.ViewBag.Jwt = jwt;
                controller.ViewBag.User = user;
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
