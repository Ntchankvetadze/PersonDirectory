using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonsDirectoryApp.Data.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string LastName { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public Enums.Gender Gender { get; set; }
        [Column(TypeName = "varchar(11)")]
        [Required]
        public string PersonalNo { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public int? CityId { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string ImageUrl { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        public List<TelephoneNumber> TelephoneNumbers { get; set; }
        public List<PersonRelationMap> PersonOneRelationMaps { get; set; }
        public List<PersonRelationMap> PersonTwoRelationMaps { get; set; }
    }
}
