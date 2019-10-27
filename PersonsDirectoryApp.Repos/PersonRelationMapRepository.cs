using Microsoft.EntityFrameworkCore;
using PersonsDirectoryApp.Data;
using PersonsDirectoryApp.Data.Models;
using PersonsDirectoryApp.Repos.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace PersonsDirectoryApp.Repos
{
    public class PersonRelationMapRepository : Repository<PersonRelationMap>, IPersonRelationMapRepository
    {
        public PersonRelationMapRepository(PersonsDirectoryDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<PersonRelationMap> GetPersonRelationMapByPerson(int personId)
        {
            //return (from p in PersonsDirectoryDbContext.PersonRelationMap
            //       where p.PersonOneId.Equals(personId) || p.PersonTwoId.Equals(personId)
            //       select p).ToList();
            return PersonsDirectoryDbContext.PersonRelationMap
                .Include(p => p.PersonOne)
                .Include(p => p.PersonTwo)
                .Where(p => p.PersonOneId.Equals(personId) || p.PersonTwoId.Equals(personId))
                .ToList();
        }

        private PersonsDirectoryDbContext PersonsDirectoryDbContext
        {
            get { return _dbContext as PersonsDirectoryDbContext; }
        }
    }
}
