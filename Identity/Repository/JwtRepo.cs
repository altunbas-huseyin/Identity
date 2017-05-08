using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{

    public class JwtRepo
    {
        private MongoDbRepository<Jwt> jwtRepository = new MongoDbRepository<Jwt>();

        public bool Add(String UserId, string Token, DateTime DeadLine)
        {
            bool result = false;
            Jwt jwt = new Jwt();
            var jwtOld = jwtRepository.SearchFor(p => p.UserId == UserId);
            if (jwtOld.Count > 0)
            {
                jwt = jwtOld[0];
                jwt.Token = Token;
                jwt.DeadLine = DeadLine;

                result = jwtRepository.Update(jwt);
            }
            else
            {

                jwt.UserId = UserId;
                jwt.Token = Token;
                jwt.DeadLine = DeadLine;
                jwt.StatusId = new Guid("1d9791fe-7a39-432f-8702-e08045e27adc");

                result = jwtRepository.Insert(jwt);
            }

            return result;
        }

        public Jwt Get(string Token)
        {
            Jwt jwt = null;
            var _token = jwtRepository.SearchFor(p => p.Token == Token);
            if (_token.Count > 0)
            {
                jwt = _token[0];
            }

            return jwt;
        }

        public bool AddUniqIndex()
        {
            bool result = jwtRepository.AddUniqIndex(new string[] { "UserId", "Token" });

            return result;
        }
    }
}
