using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pitcher.Models
{
    public class Registrations
    {
        public int ID {get;set;}
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime UserFirstName { get; set; }

        [Required]
        [Display(Name = "Is A Leader?")]
        [Column("UserIsLeader")]
        public DateTime RegistrationDate {get;set;}

        
    }
}