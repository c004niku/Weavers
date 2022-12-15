using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class UserCredentialEntity:BaseEntity
    {
//        CREATE TABLE `tblUserCredential` (
//  `Id` int (10) NOT NULL,
//   `NewPassword` varchar(500) NOT NULL,
//   `OldPassword` varchar(500) NOT NULL,
//   `LastLogin` datetime NOT NULL,
//  `Status` int (1) NOT NULL
//) ENGINE=MyISAM DEFAULT CHARSET=utf8;
    public string NewPassword { get; set; }
    public string OldPassword { get; set; }
    public DateTime LastLogin { get; set; }
    }
}