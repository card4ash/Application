using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppProj.Web.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ToLocalDateTime(this DateTime utcTime)
        {
            //return TimeZoneInfo.ConvertTimeFromUtc(utcTime, SessionHelper.TimeZone);
            return utcTime;
        }      
    }
}