using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                new User{UserFirstName="Carson",UserLastName="Alexander",UserIsLeader=false,UserContactEmail="calex51@gmail.com",UserPhoneNumber=61071800671518,UserAddress="54 Yankee st Runcorn",UserPostCode=4113,UserCountry="Australia",UserMobileNumber=610476162089,UserState="QLD",UserLogInName="BioGlands",UserPassword=")Bstd..%E=)_(:7"},
                new User{UserFirstName="Alonso",UserLastName="Meredith",UserIsLeader=true,UserContactEmail="am15@hotmail.com",UserPhoneNumber=61038002222,UserAddress="34 Webster Dandenong Park",UserPostCode=3175,UserCountry="Australia",UserMobileNumber=610423162085,UserState="VIC",UserLogInName="Bioshock32",UserPassword=")Bc--..3E=*_!j2"}                
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }
    }
}