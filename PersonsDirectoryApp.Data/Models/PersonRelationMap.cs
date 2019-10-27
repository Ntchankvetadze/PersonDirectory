using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonsDirectoryApp.Data.Models
{
    public class PersonRelationMap
    {
        [Key]
        public int Id { get; set; }
        public int PersonOneId { get; set; }
        public int PersonTwoId { get; set; }
        [Column(TypeName = "varchar(30)")]
        [Required]
        public Enums.RelationType RelationType { get; set; }

        [ForeignKey("PersonOneId")]
        public Person PersonOne { get; set; }
        [ForeignKey("PersonTwoId")]
        public Person PersonTwo { get; set; }
    }
}
