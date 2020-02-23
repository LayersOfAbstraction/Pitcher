using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Pitcher.Models
{
    public class Registration
    {
        public int ID {get;set;}
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        [Column("RegistrationDate")]
        public DateTime RegistrationDate {get;set;}
    }
}