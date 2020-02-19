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
        [StringLength(30, ErrorMessage = "First Name cannot be longer than 30 characters.")]
        [Display(Name = "Last Name")]
        public string UserLastName { get; set; }
        [Required]
        [StringLength(20,ErrorMessage = "Last Name cannot be longer than 20 characters.")]
        [Display(Name = "First Name")]
        [Column("UserFirstName")]
        public string UserFirstName { get; set; }     
        
        [Display(Name = "Is A Leader?")]
        [Column("UserIsLeader")]
        public bool UserIsLeader{get;set;}
                
        [Required]
        [StringLength(30,ErrorMessage = "Email cannot be longer than 30 characters.")]
        [Display(Name = "Email")]
        [Column("UserContactEmail")]
        public string UserContactEmail{get;set;}
        
        [StringLength(20,ErrorMessage = "Phone number cannot be longer than 20 characters.")]
        [Display(Name = "Phone Number")]
        [Column("UserPhoneNumber")]
        public long UserPhoneNumber{get;set;}
        
        [StringLength(37,ErrorMessage = "Address cannot be longer than 37 characters.")]
        [Display(Name = "Address")]
        [Column("UserAddress")]
        public string UserAddress{get;set;}

        [StringLength(4,ErrorMessage = "Post Code cannot be longer than 4 characters.")]
        [Display(Name = "Post Code")]
        [Column("UserPostCode")]
        public int UserPostCode {get;set;}

        [StringLength(15,ErrorMessage = "Country cannot be longer than 15 characters.")]
        [Display(Name = "Country")]
        [Column("UserCountry")]
        public string UserCountry {get;set;}

        [StringLength(20,ErrorMessage = "Mobile Number cannot be longer than 20 characters.")]
        [Display(Name = "Mobile Number")]
        [Column("UserMobileNumber")]
        public long UserMobileNumber {get;set;}

        [StringLength(3,ErrorMessage = "State cannot be longer than 3 characters.")]
        [Display(Name = "State")]
        [Column("UserState")]
        public string UserState {get;set;}

        [Required]
        [StringLength(18,ErrorMessage = "Username Cannot be longer than 8 characters.")]
        [Display(Name = "Username")]
        [Column("UserLogInName")]
        public string UserLogInName {get;set;}

        [Required]
        [StringLength(18,ErrorMessage = "Password Cannot be longer than 18 characters.")]
        [Display(Name = "Password")]
        [Column("UserPassword")]
        public string UserPassword {get;set;}

    }
}
