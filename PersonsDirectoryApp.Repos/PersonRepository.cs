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
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(PersonsDirectoryDbContext dbContext) 
            : base(dbContext)
        {
        }

        public Person GetPersonWithDetales(int id)
        {
            return PersonsDirectoryDbContext.Persons
                .Include(p => p.TelephoneNumbers)
                .Include(p => p.City)
                .Include(p => p.PersonOneRelationMaps)
                .Include(p => p.PersonTwoRelationMaps)
                .SingleOrDefault(p => p.Id.Equals(id));
        }

        public IEnumerable<Person> FindPersons(string searchString)
        {
            return PersonsDirectoryDbContext.Persons
                .Include(p => p.TelephoneNumbers)
                .Include(p => p.City)
                .Include(p => p.PersonOneRelationMaps)
                .Include(p => p.PersonTwoRelationMaps)
                .Where(p => p.FirstName.Contains(searchString) 
                            || p.LastName.Contains(searchString)
                            || p.PersonalNo.Contains(searchString)).ToList();
        }

        public IEnumerable<Person> GetPersonsPage(int pageIndex, int pageSize = 10)
        {
            return PersonsDirectoryDbContext.Persons
                .OrderByDescending(p => p.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public bool CheckPersonalNo(string personalNo, int personId)
        {
            return PersonsDirectoryDbContext.Persons
                .Any(p => p.PersonalNo.Equals(personalNo) && !p.Id.Equals(personId));
        }

        public Person GetPersonByPersonalNo(string personalNo)
        {
            return PersonsDirectoryDbContext.Persons
                .Include(p => p.TelephoneNumbers)
                .Include(p => p.City)
                .Include(p => p.PersonOneRelationMaps)
                .Include(p => p.PersonTwoRelationMaps)
                .SingleOrDefault(p => p.PersonalNo.Equals(personalNo));
        }

        private PersonsDirectoryDbContext PersonsDirectoryDbContext
        {
            get { return _dbContext as PersonsDirectoryDbContext; }
        }
    }
}
