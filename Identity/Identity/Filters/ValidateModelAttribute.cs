﻿using Identity.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModels.Users;
using IdentityModels.Roles;

namespace Identity.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        JwtRepo jwtRepo = new JwtRepo();
        UserRepo userRepo = new UserRepo();
        RoleRepo roleRepo = new RoleRepo();
        List<string> requiredRoleList = new List<string>();

        Microsoft.Extensions.Primitives.StringValues _Token = "";
        bool IsAcces = false;
        public ValidateModelAttribute()
        {

        }
        public ValidateModelAttribute(string _role)
        {
            requiredRoleList = _role.Split(new char[] { ',' }).ToList();
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
                string Token = "";
                Jwt jwt = new Jwt();
                try
                {
                    Token = _Token.FirstOrDefault();

                    jwt = (Jwt)jwtRepo.CheckToken(Token).Data;
                    if (jwt == null)
                    {
                        CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.OK, false, null, "Token geçersiz.");
                        BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                        context.Result = badReq;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.InternalServerError, false, null, ex.Message);
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
                        CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.OK, false, null, "Kullanıcı bulunamadı.");
                        BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                        context.Result = badReq;
                        return;
                    }

                    if (requiredRoleList.Count > 0)
                    {
                        foreach (string requiredRoleName in requiredRoleList)
                        {
                            foreach (var userRole in user.Role)
                            {
                                if (requiredRoleName == userRole.Name)
                                {
                                    Role _role = roleRepo.GetByName("1c823a7d-7475-4c09-ad13-3b94a53ca943", requiredRoleName);
                                    if (_role != null)
                                    { IsAcces = true; break; }
                                }
                            }
                        }
                        if (!IsAcces)
                        {
                            
                            CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.OK, false, null, "Yetkiniz yok.");
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
                    CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.InternalServerError, false, null, ex.Message);
                    BadRequestObjectResult badReq = new BadRequestObjectResult(response);
                    context.Result = badReq;
                    return;
                }
            }
            else
            {
                CommonApiResponse response = CommonApiResponse.Create(System.Net.HttpStatusCode.OK, false, null, "Header Token bulunamadı.");
                ObjectResult badReq = new ObjectResult(response);
                context.Result = badReq;
               
            }
        }
    }
}
