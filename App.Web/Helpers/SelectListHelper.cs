using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Helpers
{
    public static class SelectListHelper
    {
        public static SelectList ToSelectList<T>(this List<T> tbl, object selectedValue, string valueField, string textField)
        {
            if (selectedValue == null)
            {
                return new SelectList(tbl, valueField, textField);                
            }
            
            return new SelectList(tbl, valueField, textField, selectedValue);
            
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> tbl, object selectedValue, string valueField, string textField)
        {
            if (selectedValue == null)
            {
                return new SelectList(tbl, valueField, textField); 
            }

            return new SelectList(tbl, valueField, textField, selectedValue);
        }
    }
}