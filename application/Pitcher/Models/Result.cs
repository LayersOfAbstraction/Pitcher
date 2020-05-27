using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pitcher.Models
{
    public class Result
    {        
        public int JobID {get;set;}
        
        public int ProblemID {get;set;}
        public Job Job {get;set;}
        public Problem Problem {get;set;}

    }
}