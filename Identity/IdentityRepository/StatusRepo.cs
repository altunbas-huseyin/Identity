﻿using IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IdentityRepository
{
    public class StatusRepo
    {
        private MongoDbRepository<Status> statusRepository = new MongoDbRepository<Status>();
        public Guid Insert(Status status)
        {
            statusRepository.Insert(status);
            return status.Id;
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