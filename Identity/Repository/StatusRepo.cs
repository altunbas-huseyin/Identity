using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Repository
{
    public class StatusRepo
    {
        private MongoDbRepository<Models.Status> statusRepository = new MongoDbRepository<Models.Status>();
        public bool Add(Models.Status status)
        {
            return statusRepository.Insert(status);
        }

        public bool InsertStatusCodeList(Status status)
        {

            Status _status = this.GetByName(status.Name);
            if (_status == null)
            {
                statusRepository.Insert(status);
            }
            return true;
        }

        public Status GetByName(string Name)
        {
            Status status = statusRepository.SearchFor(p => p.Name == Name).FirstOrDefault();
            return status;
        }

        public bool AddUniqIndex()
        {
            bool result = statusRepository.AddUniqIndex("Name");
            return true;
        }
    }
}
