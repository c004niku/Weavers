using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weavers.Common.Models.Entities;

public static class HtmlHelpers
{
    public static string AnchorLink(this HtmlHelper html, string class_tag, string display, string stringproperty)
    {
        return string.Format("<a href='#' class=\"{0}\" {1}>{2}</a>", class_tag, stringproperty, display);
    }
}