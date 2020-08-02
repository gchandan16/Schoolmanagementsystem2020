using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.AppCode
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Displays the date in the standard format defined for the application
        /// </summary>
        /// <param name="htmlhelper"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayDate(this HtmlHelper htmlhelper, Nullable<DateTime> date)
        {
            if (date == null || date == DateTime.MinValue || date == default(DateTime) || date.Value == SqlDateTime.MinValue)
                return new MvcHtmlString(string.Empty);
            //return new MvcHtmlString(date.ToString("MMM dd, yyyy"));
            return new MvcHtmlString(date.Value.ToString("dd/MMM/yyyy hh:mm tt"));
        }

        //public static MvcHtmlString DisplayDateTime(this HtmlHelper htmlhelper, Nullable<DateTime> date)
        //{
        //    if (date == null || date == DateTime.MinValue || date == default(DateTime) || date.Value == SqlDateTime.MinValue)
        //        return new MvcHtmlString(string.Empty);
        //    //return new MvcHtmlString(date.ToString("MMM dd, yyyy"));
        //    return new MvcHtmlString(date.Value.ToString("dd/MMM/yyyy hh:mm tt"));
        //}
        /// <summary>
        /// display status
        /// </summary>
        /// <param name="htmlhelper"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayStatus(this HtmlHelper htmlhelper, string status)
        {

            if (status == "Y" || status == "y")
                return new MvcHtmlString("<b>Active</b>");
            else if (status == "N" || status == "n")
                return new MvcHtmlString("<b>InActive</b>");
            else
                return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString DisplayPOStatus(this HtmlHelper htmlhelper, int status)
        {

            if (status == 1)
                return new MvcHtmlString("Created Draft");
            else if (status == 2)
                return new MvcHtmlString("Submitted");
            else
                return new MvcHtmlString(string.Empty);
        }
    }
}