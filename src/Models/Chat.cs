using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pitcher.Models
{
    public class Chat
    {
        public int ID {get;set;}
        public int ProblemID {get;set;}

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Chat Description must be bettween 255 to 3 characters.")]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        [Column("ChatDescription")]
        public string ChatDescription {get;set;}

        [Required]
        [DataType(DataType.Date)]//AUTO INSERT.
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = " Problem Start Date")]
        [Column("ProblemStartDate")]
        public string ChatPublishDate {get;set;}

        public Problem Problem {get;set;}
    }
}