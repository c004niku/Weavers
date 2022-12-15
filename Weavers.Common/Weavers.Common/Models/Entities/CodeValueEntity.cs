using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class CodeValueEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string NameToDisplay { get; set; }
        public string IconUrl { get; set; }

        public string UserType { get; set; }
    }

    public class CodeEntity:CodeValueEntity
    {
        public string ProfileImage { get; set; }
        public string MobileNumber { get; set; }

        public string Email { get; set; }
    }
}