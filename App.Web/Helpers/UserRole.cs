using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppProj.Domain;


namespace AppProj.Web.Helpers
{
    public static class UserRole
    {
        public static bool Check(string featureName, object roleData)
        {
            //return true;

            List<string> roles = null;
            try
            {
                roles = (List<string>)roleData;
            }
            catch { }
            finally
            {
                roles = roles ?? new List<string>();
            }

            int i = roles.IndexOf(featureName);

            if (i >= 0) return true;

            return false;
        }
    }
}