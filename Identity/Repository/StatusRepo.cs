using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class StatusRepo
    {
        private MongoDbRepository<Models.Status> statusRepository = new MongoDbRepository<Models.Status>();
        public bool Add(Models.Status status)
        {
            return statusRepository.Insert(status);
        }

        public bool InsertStatusCodeList()
        {
            statusRepository.Insert(new Models.Status { Name = "Active" });
            statusRepository.Insert(new Models.Status { Name = "Passive" });
            statusRepository.Insert(new Models.Status { Name = "Waiting" });
            statusRepository.Insert(new Models.Status { Name = "Deleted" });
            return true;
        }


        public bool AddUniqIndex()
        {
            bool result = statusRepository.AddUniqIndex("Name");
            return true;
        }
    }
}
