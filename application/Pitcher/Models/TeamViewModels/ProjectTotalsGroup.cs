using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Pitcher.Data;

namespace Pitcher.Models.TeamViewModels
{
    public class ProjectTotalsGroup
    {
        [DataType(DataType.Text)]
        public string JobName{get;set;}

        public int UserCount{get;set;}

        public int ProblemCount{get;set;}

        [DataType(DataType.Date)]
        public DateTime? DeadlineDate { get; set; }


    }
}