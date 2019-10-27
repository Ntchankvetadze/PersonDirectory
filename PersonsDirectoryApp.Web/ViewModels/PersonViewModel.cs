using Microsoft.AspNetCore.Http;
using PersonsDirectoryApp.Data.Models;
using PersonsDirectoryApp.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsDirectoryApp.Web.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z]{2,50}|[\u10D0-\u10F0]{2,50}", ErrorMessage = "First Name must be min 2 and max 50 eng or geo characters")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("[a-zA-Z]{2,50}|[\u10D0-\u10F0]{2,50}", ErrorMessage = "First Name must be min 2 and max 50 eng or geo characters")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public Enums.Gender Gender { get; set; }
        [Required]
        [RegularExpression("[0-9]{11}", ErrorMessage ="Personal No must be contains 11 numerics")]
        [DisplayName("Personal No")]
        public string PersonalNo { get; set; }
        [Required]
        [MinimumAge(18, ErrorMessage = "Person must be someone at least 18 years of age")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        public DateTime BirthDate { get; set; }
        [DisplayName("City")]
        public int? CityId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Photo { get; set; }

        public City City { get; set; }
        [DisplayName("Telephones")]
        public List<TelephoneNumber> TelephoneNumbers { get; set; }
        public List<PersonRelationMap> PersonRelationMaps { get; set; }

        public string CityFullName { get { if (City != null) return $"{City.Name}/{City.Country}"; else return ""; } }
        [DisplayName("Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public int Age { get { return DateTime.Today.Year - BirthDate.Year; } }
    }
}
