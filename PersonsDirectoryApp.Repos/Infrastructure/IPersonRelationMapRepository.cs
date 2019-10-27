using PersonsDirectoryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectoryApp.Repos.Infrastructure
{
    public interface IPersonRelationMapRepository : IRepository<PersonRelationMap>
    {
        IEnumerable<PersonRelationMap> GetPersonRelationMapByPerson(int personId);
    }
}
