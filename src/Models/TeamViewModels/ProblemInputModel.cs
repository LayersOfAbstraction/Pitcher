

namespace Pitcher.Models
{
    public class ProblemInputModel
    {  
        [DataType(DataType.Upload)]
        [Display(Name = " Upload file")]
        [Column("ProblemFileAttachments")]
        public List<IFormFile> ProblemFileAttachments {get;set;}
    }
}