using Microsoft.EntityFrameworkCore;
using PersonsDirectoryApp.Data;
using PersonsDirectoryApp.Data.Models;
using PersonsDirectoryApp.Repos.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PersonsDirectoryApp.Repos
{
    public class TelephoneNumberRepository : Repository<TelephoneNumber>, ITelephoneNumberRepository
    {
        public TelephoneNumberRepository(PersonsDirectoryDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<TelephoneNumber> GetTelephoneNumbersByPerson(int personId)
        {
            return from t in PersonsDirectoryDbContext.TelephoneNumbers.Include(t => t.Person)
                    where t.PersonId.Equals(personId) select t;
        }

        public TelephoneNumber GetTelephoneNumberByNumber(int personId, string Number)
        {
            return PersonsDirectoryDbContext.TelephoneNumbers.Include(t => t.Person)
                    .FirstOrDefault(t => t.Number.Equals(Number) && t.PersonId.Equals(personId));
        }

        private PersonsDirectoryDbContext PersonsDirectoryDbContext
        {
            get { return _dbContext as PersonsDirectoryDbContext; }
        }
    }
}
