using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pitcher.Models.TeamViewModels
{
    public class JobIndexData
    {
        public IEnumerable<Job> Jobs { get; set; }
        public IEnumerable<Problem> Problems { get; set; }

        public IEnumerable<Result> Results { get; set; }
    }
}