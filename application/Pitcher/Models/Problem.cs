using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pitcher.Models
{
    public class Problem
    {
        public int ID {get;set;}
        
        [Required]
        [StringLength(180, MinimumLength = 2, ErrorMessage = "Problem Title must be bettween 2 to 20 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Problem Title")]
        [Column("ProblemTitle")]
        public string ProblemTitle {get;set;}

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 5, ErrorMessage = "Problem Title must be at least 5 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Problem Description")]
        [Column("ProblemDescription")]
        public string ProblemDescription {get;set;}

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = " Problem Start Date")]
        [Column("ProblemStartDate")]
        public DateTime ProblemStartDate {get;set;}

        [DataType(DataType.Upload)]
        [Display(Name = " Upload file")]
        [Column("ProblemFileAttachments")]
        public string ProblemFileAttachments {get;set;}

        [Required]
        [Display(Name = "Problem Severity")] 
        [Range(1,5, ErrorMessage
             = "Problem Severity value for {0} must be between {1} and {2}.")]       
        [Column("ProblemSeverity")]
        public int ProblemSeverity {get;set;}

        [Display(Name = "Problem Complete")]        
        [Column("ProblemComplete")]        
        public bool ProblemComplete {get;set;}
        
        public ICollection<Result> Result {get;set;}

        public ICollection<Chat> Chat {get;set;}
    }
}