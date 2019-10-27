using PersonsDirectoryApp.Data.Models;
using PersonsDirectoryApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsDirectoryApp.Web.Models
{
    public class PersonsSearchModel
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public Enums.Gender? Gender { get; set; }
        [DisplayName("Personal No")]
        public string PersonalNo { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        public DateTime? BirthDate { get; set; }
        [DisplayName("City")]
        public int? CityId { get; set; }
        [DisplayName("Has Photo")]
        public bool? HasPhoto { get; set; }
        public List<PersonViewModel> PersonsList { get; set; }

        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling(TotalCount / (double)PageSize); } }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public void GetPage(List<PersonViewModel> persons)
        {
            TotalCount = persons.Count();
            PersonsList = persons.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

        }
    }
}
