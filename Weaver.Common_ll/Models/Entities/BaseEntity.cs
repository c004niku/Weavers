using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public int Status { get; set; }

        public bool IsActive { get { return Status == 1; } }
    }
}