using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonsDirectoryApp.Data.Models
{
    public class TelephoneNumber
    {
        [Key]
        public int Id { get; set; }
        public int PersonId { get; set; }
        [Column(TypeName = "varchar(30)")]
        [Required]
        public Enums.TelephoneType TelephoneType { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Number { get; set; }
        
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
    }
}
