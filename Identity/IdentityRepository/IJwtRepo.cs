using IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityRepository
{
    public interface IJwtRepo<T> : IBaseRepo<Jwt>
    {
        Result CheckToken(string Token);
        bool AddUniqIndex();
    }
}
