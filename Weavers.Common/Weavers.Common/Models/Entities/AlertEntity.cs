using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class AlertEntity: BookMark
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public Collection<string> Images { get; set; }

        public string Url { get; set; }

        public UserShops CreatedBy { get; set; }

    }

    public class NewsEntity: BookMark
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public Collection<string> Images { get; set; }

        public UserShops CreatedBy { get; set; }

        public string Url { get; set; }

    }

    public class VideoEntity: BookMark
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public UserShops CreatedBy { get; set; }


        public string Url { get; set; }

        public Collection<string> Images { get; set; }

    }

    public class BookMark
    {
        public bool IsBookMarked { get; set; }

        public string CreatedOn { get; set; }

        [JsonIgnore]
        public DateTime ParsedDateValue
        {
            get
            {
                var dt = this.CreatedOn;
                var value = DateTime.Now.Date;
                DateTime.TryParse(dt, out value);
                return value;
            }
        }

        public string Range { get; set; }
    }
}