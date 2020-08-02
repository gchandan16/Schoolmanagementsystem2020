using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace WEB.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OwnerId { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public bool IsBlock { get; set; }

        ////public int LogFAttamps { get; set; }
        ////public DateTime LogFDT { get; set; }
        [Required]
        [Display(Name = "Financial Year")]
        public int FID { get; set; }
    }
    public class SignupModel
    {
        [Required(ErrorMessage = "Please Enter Organization")]
        public string Oname { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string BOAddress { get; set; }

        public string BOAddress2 { get; set; }


        public string BOPincode { get; set; }

       

        public string LDistict { get; set; }

        public string LArea { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        [Remote("EmailExists", "Account", HttpMethod = "POST", ErrorMessage = "Email address already registered.")]
        public string LEmailId { get; set; }
        [Required(ErrorMessage = "Please enter mobile number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid mobile number")]

        public string LMobile { get; set; }

        public string LPhone { get; set; }

        public string LWebsite { get; set; }

        public string OAfficilate { get; set; }

        public string OlicNo { get; set; }

        public string OTaxNo { get; set; }

        public string OPanNo { get; set; }
        [Required(ErrorMessage = "Please Enter Contact Person")]
        public string OContactNo { get; set; }

        [Required(ErrorMessage = "Please select Country")]
        public int COUNTRY_ID { get; set; }
        [Required(ErrorMessage = "Please select State")]
        public int STATE_ID { get; set; }
        [Required(ErrorMessage = "Please select City")]
        public int CITY_ID { get; set; }
        public List<CountryMaster> CountryList { get; set; }
        public List<StateMaster> StateList { get; set; }
        public List<CityMaster> CityList { get; set; }
    }

    public class ResetForgotPassword : BaseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        //Add new columns 
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string IMAGE { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }

    }
}
