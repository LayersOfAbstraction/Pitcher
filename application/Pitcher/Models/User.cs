using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Pitcher.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "* Last Name be bettween 2 to 30 characters.")]
        [Display(Name = "Last Name")]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "* First Name be bettween 2 to 20 characters.")]
        [Display(Name = "First Name")]
        [Column("UserFirstName")]
        public string UserFirstName { get; set; }     
        
        [Display(Name = "Is A Leader?")]
        [Column("UserIsLeader")]
        public bool UserIsLeader{get;set;}
                
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Email address be bettween 3 to 30 characters.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Column("UserContactEmail")]
        public string UserContactEmail{get;set;}
        
        [StringLength(20, MinimumLength = 5,ErrorMessage = "Phone Number must be bettween 5 to 30 characters." +
            "Advise you input the full international number if user is out of state.")]
        [Display(Name = "Phone Number")]
        [Column("UserPhoneNumber")]
        public long UserPhoneNumber{get;set;}
        
        [StringLength(37,ErrorMessage = "Address cannot be longer than 37 characters.")]
        [Display(Name = "Address")]
        [Column("UserAddress")]
        public string UserAddress{get;set;}

        [StringLength(32, MinimumLength = 2, ErrorMessage = "Post Code must be bettween 2 to 32 characters.")]
        [Display(Name = "Post Code")]
        [Column("UserPostCode")]
        public int UserPostCode {get;set;}

        [StringLength(15,ErrorMessage = "Country cannot be longer than 15 characters.")]
        [Display(Name = "Country")]
        [Column("UserCountry")] 
        public string UserCountry {get;set;}

        [StringLength(20, MinimumLength = 5, ErrorMessage = "Mobile Number must be bettween 5 to 20 characters." +
            "Advise you input the full international number if user is out of state.")]
        [Display(Name = "Mobile Number")]
        [Column("UserMobileNumber")]
        public long UserMobileNumber {get;set;}

        [StringLength(3,ErrorMessage = "State cannot be longer than 3 characters.")]
        [Display(Name = "State")]
        [Column("UserState")]
        public string UserState {get;set;}

        [Required]
        [StringLength(16, MinimumLength = 1,ErrorMessage = "* Username must be bettween 1 to 16 characters.")]
        [Display(Name = "Username")]
        [Column("UserLogInName")]
        public string UserLogInName {get;set;}

        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "* Password Cannot be longer than 18 characters.")]
        [Display(Name = "Password")]
        [Column("UserPassword")]
        public string UserPassword {get;set;}

    }
}
