using PersonsDirectoryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsDirectoryApp.Web.ViewModels
{
    public class TelephoneNumberViewModel
    {
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        public Enums.TelephoneType TelephoneType { get; set; }
        [Required]
        [RegularExpression(@"([/^+\d]+){4,50}", ErrorMessage = "Number must contain \"+\" or numbers, length 4-50")]
        public string Number { get; set; }
    }
}
