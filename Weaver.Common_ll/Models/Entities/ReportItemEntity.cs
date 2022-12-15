using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Newtonsoft;
using Newtonsoft.Json;

namespace Weavers.Common.Models.Entities
{
    public class ReportItemEntity:BaseEntity
    {

        //`UserId`, `ReportedOn`, `ShortDescription`, `Status`, `Action`, `DetailDescription`

        public int UserId { get; set; }

        public ReportType ReportType { get; set; }

        public string ReportedOn { get; set; }

        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }

        public string Images { get; set; }

        public string Url { get; set; }

        public string Action { get; set; }

        public bool IsBookMarked { get; set; }

        [JsonIgnore]
        public DateTime ParsedReportedValue { get {
                var dt = this.ReportedOn;
                var value = DateTime.Now.Date;
                DateTime.TryParse(dt, out value);
                return value;
            } }

        public new ReportStatusType Status { get; set; }

        public string Range { get; set; }

    }

    public enum ReportType
    {
        news,
        alert,
        videos,
        shops,
        buyer,
        sales,
        request,
        member
    }

    public enum ReportStatusType
    {
        PENDING,
        INPROGRESS,
        ACCEPTED,
        REJECTED
    }
}