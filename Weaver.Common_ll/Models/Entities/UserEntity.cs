using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class UserEntity
    {
        public UserBasicEntity userBasicEntity { get; set; }

        public UserProfessionalDetail userProfessionalDetail { get; set; }
    }
}