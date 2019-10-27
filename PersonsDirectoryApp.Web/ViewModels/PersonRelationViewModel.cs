using PersonsDirectoryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsDirectoryApp.Web.ViewModels
{
    public class PersonRelationMapViewModel
    {
        public int Id { get; set; }
        [Required]
        [Compare(nameof(PersonOneId), ErrorMessage = "Related persons must be different")]
        public int PersonOneId { get; set; }
        public int PersonTwoId { get; set; }
        [Required]
        public Enums.RelationType RelationType { get; set; }
        [Required]
        [RegularExpression("[0-9]{11}", ErrorMessage = "Personal No must be contains 11 numerics")]
        public string NewRelativePersonalNo { get; set; }
    }
}
