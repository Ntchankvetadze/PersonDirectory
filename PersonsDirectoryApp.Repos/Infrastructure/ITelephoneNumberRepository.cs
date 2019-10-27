using PersonsDirectoryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectoryApp.Repos.Infrastructure
{
    public interface ITelephoneNumberRepository : IRepository<TelephoneNumber>
    {
        IEnumerable<TelephoneNumber> GetTelephoneNumbersByPerson(int personId);
        TelephoneNumber GetTelephoneNumberByNumber(int personId, string Number);
    }
}
