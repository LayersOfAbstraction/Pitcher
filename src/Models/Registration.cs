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
        [Display(Name = "User Start Date")]
        [Column("RegistrationDate")]
        public DateTime RegistrationDate {get;set;}        
        public User User {get;set;}
        public Job Job {get;set;}
    }
}