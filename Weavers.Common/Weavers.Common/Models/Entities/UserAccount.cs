using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class UserAccount
    {
        public UserBasicEntity userBasicEntity { get; set; }

        public UserCredentialEntity userCredentialEntity { get; set; }

        public UserProfessionalDetail userProfessionalDetail { get; set; }
    }

    public class UserUpdateAccount
    {
        public UserBasicEntity userBasicEntity { get; set; }


        public UserProfessionalDetail userProfessionalDetail { get; set; }
    }

    public class UserUpdateLanguage
    {
        public int UserId { get; set; }
        public int LangId { get; set; }
    }

    public class UserUpdateFullName
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserUpdateType
    {
        public EnumUpdateType? UpdateType { get; set; }

        public string UserMobile { get; set; }

        public int userId { get; set; }

        public string UpdateValue { get; set; }
    }

}