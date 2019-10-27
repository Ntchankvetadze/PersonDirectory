using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectoryApp.Repos.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository Persons { get; }
        ITelephoneNumberRepository TelephoneNumbers { get; }
        IPersonRelationMapRepository PersonRelationMaps { get; }
        ICityRepository Cities { get; }
        
        void CreateTransaction();
        void Commit();
        void RollBack();
        int Complete();
    }
}
