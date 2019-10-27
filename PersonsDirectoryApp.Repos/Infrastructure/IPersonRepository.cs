using PersonsDirectoryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonsDirectoryApp.Repos.Infrastructure
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetPersonWithDetales(int id);
        Person GetPersonByPersonalNo(string personalNo);
        IEnumerable<Person> GetPersonsPage(int pageIndex, int pageSize);
        IEnumerable<Person> FindPersons(string searchString);
        bool CheckPersonalNo(string personalNo, int personId);
    }
}
