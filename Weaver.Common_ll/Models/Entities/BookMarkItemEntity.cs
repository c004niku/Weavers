using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class BookMarkItemEntity
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }

        public ReportType Type { get; set; }

        public bool BookMarked { get; set; }
    }

    public class BookMarkList
    {
        public ICollection<BookMarkItem> BookMarkItems { get; set; }
    }

    public class BookMarkItem
    {
        public ReportType Type { get; set; }
        public List<CodeValueEntity> BookMarkItems { get; set; }
    }


    public class BookMarkCountItem
    {
        public string Type { get; set; }
        public int Count { get; set; }
    }
}