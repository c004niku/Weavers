using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class SubCategoryEntity:BaseEntity
    {

//        CREATE TABLE `tblSubCategory` (
//  `SubCategoryId` int (10) NOT NULL,
//   `CategoryId` int (10) NOT NULL,
//    `SubCategoryName` varchar(500) NOT NULL,
//    `Status` int (1) NOT NULL
//) ENGINE=MyISAM DEFAULT CHARSET=utf8;
        public CodeValueEntity Category { get; set; }
        public string Name { get; set; }

        public string Telugu { get; set; }

        public string Tamil { get; set; }
        public string Hindi { get; set; }
        public string kannad { get; set; }

    }
}