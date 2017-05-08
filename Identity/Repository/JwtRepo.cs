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
            Jwt jwt = new Jwt();
            var jwtOld = jwtRepository.SearchFor(p => p.UserId == UserId);
            if (jwtOld.Count > 0)
            {
                jwt = jwtOld[0];
                jwt.Token = Token;
                jwt.DeadLine = DeadLine;
                jwt.UpdateDate = DateTime.Now;

                jwtRepository.Update(jwt);
            }
            else
            {
                
                jwt.UserId = UserId;
                jwt.Token = Token;
                jwt.DeadLine = DeadLine;
                jwt.CreateDate = DateTime.Now;
                jwt.StatusId = new Guid("1d9791fe-7a39-432f-8702-e08045e27adc");

                jwtRepository.Insert(jwt);
            }


            return true;
        }

        public bool AddUniqIndex()
        {
            bool result = jwtRepository.AddUniqIndex("UserId");
            return true;
        }
    }
}
