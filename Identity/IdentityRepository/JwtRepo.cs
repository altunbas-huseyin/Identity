
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IdentityModels;
using Microsoft.Extensions.Configuration;

namespace IdentityRepository
{

    public class JwtRepo : BaseRepo<Jwt>
    {
        private Result result = new Result();
        private StatusRepo statusRepo;

        public JwtRepo(IConfiguration configuration) : base(configuration)
        {
            statusRepo = new StatusRepo(configuration);
        }

        public Result Add(String UserId, string Token, DateTime DeadLine)
        {
            Status status = statusRepo.GetByName("Active");

            Jwt jwt = new Jwt();
            var jwtOld = mongoContext.SearchFor(p => p.User_Id == UserId);
            if (jwtOld.Count > 0)
            {
                jwt = jwtOld[0];
                jwt.Token = Token;
                jwt.Dead_Line = DeadLine;

                mongoContext.Update(jwt);
            }
            else
            {

                jwt.User_Id = UserId;
                jwt.Token = Token;
                jwt.Dead_Line = DeadLine;
                jwt.Status_Id = status.Id;

                mongoContext.Insert(jwt);
            }
            result = new Result(jwt, true);
            return result;
        }

        public Result CheckToken(string Token)
        {
            Jwt jwt = null;
            var _token = mongoContext.SearchFor(p => p.Token == Token);
            if (_token.Count > 0)
            {
                jwt = _token[0];

                if (jwt.Dead_Line < DateTime.Now)
                {
                    jwt = null;
                }
            }

            return new Result(jwt, true);
        }

        public Result GetByUserId(string UserId)
        {
            Jwt jwt = mongoContext.SearchFor(p => p.User_Id == UserId).FirstOrDefault();
            return result = new Result(jwt, true);
        }

        public bool AddUniqIndex()
        {
            bool result = mongoContext.AddUniqIndex(new string[] { "UserId", "Token" });

            return result;
        }
    }
}
