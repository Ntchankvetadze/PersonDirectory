using PersonsDirectoryApp.Data;
using PersonsDirectoryApp.Repos.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectoryApp.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonsDirectoryDbContext _dbContext;

        public UnitOfWork(PersonsDirectoryDbContext dbContext)
        {
            _dbContext = dbContext;
            Persons = new PersonRepository(_dbContext);
            TelephoneNumbers = new TelephoneNumberRepository(_dbContext);
            PersonRelationMaps = new PersonRelationMapRepository(_dbContext);
            Cities = new CityRepository(_dbContext);
        }

        public IPersonRepository Persons { get; private set; }
        public ITelephoneNumberRepository TelephoneNumbers { get; private set; }
        public IPersonRelationMapRepository PersonRelationMaps { get; private set; }
        public ICityRepository Cities { get; private set; }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void CreateTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
    }
}
