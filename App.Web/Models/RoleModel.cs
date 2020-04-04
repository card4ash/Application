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
    public class RoleModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Insert Role Name.")]
        [Display(Name = "Name")]
        [MaxLength(100)]
        public string RoleName { get; set; }

        [Required]
        [Display(Name = "Default Page")]
        public int RoleDefaultPageId { get; set; }
        public IEnumerable<SelectListItem> RoleDefaultPages { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public bool CanAccessAllProjects { get; set; }
    }
}
