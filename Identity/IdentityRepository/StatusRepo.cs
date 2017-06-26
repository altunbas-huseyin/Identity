using IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IdentityRepository
{
    public class StatusRepo : BaseRepo<Status>
    {
        public string Insert(Status status)
        {
            mongoContext.Insert(status);
            return status.Id;
        }

        public Status GetByName(string Name)
        {
            Status status = mongoContext.SearchFor(p => p.Name == Name).FirstOrDefault();
            return status;
        }

        public bool AddUniqIndex()
        {
            bool result = mongoContext.AddUniqIndex("Name");
            return true;
        }
    }
}
