﻿using Identity.Middleware;
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
        JwtRepo jwtRepo = new JwtRepo();
        UserRepo userRepo = new UserRepo();
        RoleRepo roleRepo = new RoleRepo();
        List<string> Role = new List<string>();

        Microsoft.Extensions.Primitives.StringValues _Token = "";
        bool IsAcces = false;
        public ValidateModelAttribute()
        {

        }
        public ValidateModelAttribute(string _role)
        {
            Role = _role.Split(new char[] { ',' }).ToList();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {


            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }


            context.HttpContext.Request.Headers.TryGetValue("Token", out _Token);
            if (_Token.Count > 0)
            {
                string Token = _Token.FirstOrDefault();

                Jwt jwt = jwtRepo.CheckToken(Token);
                if (jwt == null)
                {
                    CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.BadRequest, null, "Token geçersiz.");
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                    return;
                }

                try
                {
                    var controller = context.Controller as Controller;
                    User user = userRepo.GetById(jwt.UserId);
                    if (user == null)
                    {
                        CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.BadRequest, null, "Kullanıcı bulunamadı.");
                        BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                        context.Result = badReq;
                        return;
                    }
                    if (Role.Count > 0)
                    {
                        foreach (string item in Role)
                        {
                            if (user.Role.Where(p => p.Name == item).Count() > 0)
                            {
                                IsAcces = true;
                            }
                        }
                        if (!IsAcces)
                        {
                            CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.BadRequest, null, "Yetkiniz yok.");
                            BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                            context.Result = badReq;
                            return;
                        }
                    }

                    controller.ViewBag.Jwt = jwt;
                    controller.ViewBag.User = user;
                }
                catch (Exception ex)
                {
                    CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.BadRequest, null, "Hata oluştu");
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                    return;
                }
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
