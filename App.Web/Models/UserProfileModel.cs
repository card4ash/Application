using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AppProj.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppProj.Web.Models
{
    public class UserProfileModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Insert BRAC PIN / Login User Name.")]
        [Display(Name = "PIN / User Id")]
        public string Pin { get; set; }
        
        [Required(ErrorMessage = "Please Insert Full Name.")]
        [Display(Name = "Full Name")]
        [MaxLength(100)]
        public string UserName { get; set; }
                
        [Display(Name = "Email Address")]
        [MaxLength(100)]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Insert Password")]
        [Display(Name = "Current Password")]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Insert New Password")]
        [Display(Name = "New Password")]
        [MaxLength(100)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Insert Confirm Password")]
        [Display(Name = "Confirm Password")]
        [System.Web.Mvc.CompareAttribute("NewPassword", ErrorMessage = "Passwords don't match.")]
        public string ConfirmNewPassword { get; set; }
        
        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        [Column(TypeName = "image")]
        [MaxLength(int.MaxValue)]
        public byte[] Photo { get; set; }
        
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }

    public class UserDistrictModel
    {
        public int UserInfoId { get; set; }
        public IEnumerable<DistrictByUserProfile> DistrictByUserProfile { get; set; }
        public IEnumerable<StandingData> RestDistricts { get; set; }
    }
}