using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pitcher.Models
{
    public class Registration
    {
        public int ID {get;set;}
        public int UserID { get; set; }
        public int JobID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime UserFirstName { get; set; }

        [Required]
        [Display(Name = "Is A Leader?")]
        [Column("UserIsLeader")]
        public DateTime RegistrationDate {get;set;}

        public User Users {get;set;}
        public Job Jobs {get;set;}
    }
}