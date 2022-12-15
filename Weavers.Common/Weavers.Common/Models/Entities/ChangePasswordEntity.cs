using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class ForgotPasswordEntity:BaseEntity
    {
        public string MobileNumber { get; set; }

        public string Otp { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

    }

    public class ChangePasswordEntity : ForgotPasswordEntity
    {
        public string OldPassword { get; set; }
    }
}