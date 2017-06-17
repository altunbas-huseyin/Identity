
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IdentityModels;

namespace IdentityRepository
{

    public class JwtRepo : BaseRepo<Jwt>, IBaseRepo<Jwt>
    {
        
        private StatusRepo statusRepo = new StatusRepo();
        public Jwt Add(String UserId, string Token, DateTime DeadLine)
        {
            Status status = statusRepo.GetByName("Active");

            Jwt jwt = new Jwt();
            var jwtOld = mongoContext.SearchFor(p => p.UserId == UserId);
            if (jwtOld.Count > 0)
            {
                jwt = jwtOld[0];
                jwt.Token = Token;
                jwt.DeadLine = DeadLine;

                mongoContext.Update(jwt);
            }
            else
            {

                jwt.UserId = UserId;
                jwt.Token = Token;
                jwt.DeadLine = DeadLine;
                jwt.Status = status;

                mongoContext.Insert(jwt);
            }

            return jwt;
        }

        public Jwt CheckToken(string Token)
        {
            Jwt jwt = null;
            var _token = mongoContext.SearchFor(p => p.Token == Token);
            if (_token.Count > 0)
            {
                jwt = _token[0];

                if (jwt.DeadLine < DateTime.Now)
                {
                    jwt = null;
                }
            }

            return jwt;
        }

        public Jwt GetByUserId(string UserId)
        {
            Jwt jwt = mongoContext.SearchFor(p => p.UserId == UserId).FirstOrDefault();
            return jwt;
        }

        public bool AddUniqIndex()
        {
            bool result = mongoContext.AddUniqIndex(new string[] { "UserId", "Token" });

            return result;
        }

        public Result Add(Jwt entity)
        {
            throw new NotImplementedException();
        }

        public Result Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Result Update(Jwt entity)
        {
            throw new NotImplementedException();
        }

        public List<Jwt> GetAll(string UserId)
        {
            throw new NotImplementedException();
        }

        public Jwt FindById(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
