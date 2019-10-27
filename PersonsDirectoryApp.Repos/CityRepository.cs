using Microsoft.EntityFrameworkCore;
using PersonsDirectoryApp.Data;
using PersonsDirectoryApp.Data.Models;
using PersonsDirectoryApp.Repos.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectoryApp.Repos
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(DbContext dbContext) : base(dbContext)
        {
        }

        private PersonsDirectoryDbContext PersonsDirectoryDbContext
        {
            get { return _dbContext as PersonsDirectoryDbContext; }
        }
    }
}
