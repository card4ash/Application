using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppProj.Web.Helpers
{
    public static class SsoUtility
    {
        public const string SsoToken = "multipass";
        public const string UserId = "UserId";
        public static readonly string Site = System.Web.Configuration.WebConfigurationManager.AppSettings["site"].ToString();//"SMSC-DEV"
        public static readonly string SsoLogin = System.Web.Configuration.WebConfigurationManager.AppSettings["SSOLogin"].ToString();
        public static readonly string SsoLogout = ConfigurationManager.AppSettings["SSOLogout"].ToString();
        public static readonly string SsoLogoutReq = SsoLogout + "?site=" + Site;
        public static readonly string AbsoluteUri = ConfigurationManager.AppSettings["AbsoluteUri"].ToString();
        public static readonly string SsoSession = ConfigurationManager.AppSettings["SSOSession"].ToString();
        public const string ProjectId = "ProjectId";


        public enum Request
        {
            normal = 0,
            Login,
            Logout,
            session
        };
    }
}