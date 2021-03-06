﻿using System;
using System.Linq;
using Pitcher.Models;

namespace Pitcher.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TeamContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{ UserFirstName="Carson",UserLastName="Alexander",UserContactEmail="jnash486+test1@gmail.com",UserPhoneNumber="61071800671518",UserAddress="54 Yankee st Runcorn",UserPostCode="4113",UserCountry="Australia",UserMobileNumber="0476162089",UserState="QLD"},
                new User{ UserFirstName="Alonso",UserLastName="Meredith",UserContactEmail="jnash486+am15@gmail.com",UserPhoneNumber="61038002222",UserAddress="34 Webster Dandenong Park",UserPostCode="3175",UserCountry="Australia",UserMobileNumber="0423162085",UserState="VIC"},
                new User{ UserFirstName="Arturo",UserLastName="Anand",UserContactEmail="jnash486+aa01@gmail.com",UserPhoneNumber="61028004278",UserAddress="72 Doug Rd Wakevale",UserPostCode="3115",UserCountry="Australia",UserMobileNumber="0423162085",UserState="VIC"},
                new User{ UserFirstName="Gytis",UserLastName="Barzdukas",UserContactEmail="	jnash486+gytisB@gmail.com", UserPhoneNumber="61027104224",UserAddress="156 Barnett Rd",UserPostCode="4060",UserCountry="Austraila",UserMobileNumber="0469162074",UserState="VIC"},                         
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var registrations = new Registration[]
            {
                new Registration{UserID=1, JobID=1, RegistrationDate=DateTime.Parse("2005-09-01")},
                new Registration{UserID=2, JobID=2, RegistrationDate=DateTime.Parse("2004-03-07")},
                new Registration{UserID=3, JobID=3, RegistrationDate=DateTime.Parse("2004-02-13")}
            };

            foreach (Registration r in registrations)
            {
                context.Registrations.Add(r);
            }
            context.SaveChanges();

            //This means projects
            var jobs = new Job[]
            {
                new Job{JobTitle="Eskate Tracker",JobDescription="A fitness tracker designed for inline skaters.",JobStartDate=DateTime.Parse("2004-03-07"),JobDeadline=DateTime.Parse("2003-12-07"),JobIsComplete=false},
                new Job{JobTitle="FTDC",JobDescription="A windows desktop app that renames files with time and date.",JobStartDate=DateTime.Parse("2004-03-07"),JobDeadline=DateTime.Parse("2003-08-07"),JobIsComplete=false},
                new Job{JobTitle="Pitcher",JobDescription="A Picture viewer to replace the default Windows picture viewer.",JobStartDate=DateTime.Parse("2020-02-10"),JobDeadline=DateTime.Parse("2020-07-2"), JobIsComplete=false}                                                              
            };

            foreach (Job j in jobs)
            {
                context.Jobs.Add(j);
            }
            context.SaveChanges();

            var problems = new Problem[]
            {
                new Problem{ProblemTitle="Can't ouput creation date in long format",ProblemDescription="filename I am trying to move. The problem I am having though is it only outputs the seconds including AM or PM to the file.",ProblemStartDate=DateTime.Parse("2004-03-07"),ProblemFileAttachments="blah",ProblemSeverity=5,ProblemComplete=true}                
            };

            foreach (Problem p in problems)
            {
                context.Problems.Add(p);
            }
            context.SaveChanges();
        }
    }
}