using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weavers.Common.Models.Entities;

namespace WeaverAdmin.Models
{
    public class ReportItem : ReportItemEntity
    {      
        [AllowHtml]
        public new string ShortDescription { get; set; }
        [AllowHtml]
        public new string DetailDescription { get; set; }
    }
}