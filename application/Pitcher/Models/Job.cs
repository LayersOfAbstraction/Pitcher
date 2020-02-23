using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pitcher.Models
{
    public class Job
    {        
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Job must be bettween 3 to 20 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Job Title")]
        [Column("JobTitle")]
        public string JobTitle { get; set; }

        [StringLength(200, MinimumLength = 71, ErrorMessage = "Last Name must be bettween 200 to 71 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Job Description")]
        [Column("JobDescription")]
        public string JobDescription { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        [Column("JobStartDate")]
        public DateTime JobStartDate {get;set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deadline Date")]
        [Column("JobDeadlineDate")]
        public DateTime JobDeadline {get;set;}

        [Display(Name = "Job Is Complete?")]
        [Column("JobIsComplete")]
        public bool JobIsComplete{get;set;}

        public ICollection<Registration> Registrations {get;set;}
    }
}
