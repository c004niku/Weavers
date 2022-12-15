using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class UserBasicEntity
    {
//        CREATE TABLE `tblUserBasic` (
//  `Id` int (10) NOT NULL,
//   `FirstName` varchar(500) NOT NULL,
//   `LastName` varchar(500) NOT NULL,
//   `Email` varchar(500) NOT NULL,
//   `UserName` varchar(500) NOT NULL,
//   `MobileNumber` varchar(10) NOT NULL
//) ENGINE=MyISAM DEFAULT CHARSET=utf8;
        public int ID { get; set; }
        public string FullName { get; set; }
        //public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }

        public int LangId { get; set; }

        public string UserType { get; set; }

        public string Otp { get; set; }
    }
}