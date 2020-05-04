namespace Pitcher.Models
{
    public class Chat
    {
        public int ID {get;set;}
        public int ProblemID {get;set;}

        public int RegistrationID {get;set;}

        public string ChatDescription {get;set;}

        public string ChatPublishDate {get;set;}

        public Registration Registration {get;set;}

        public Problem Problem {get;set;}
    }
}