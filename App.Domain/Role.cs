//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppProj.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Role
    {
        public Role()
        {
            this.RoleFeatures = new HashSet<RoleFeature>();
            this.UserProfiles = new HashSet<UserProfile>();
        }
    
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int RoleDefaultPageId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual RoleDefaultPage RoleDefaultPage { get; set; }
        public virtual ICollection<RoleFeature> RoleFeatures { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
