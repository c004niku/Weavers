using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class OccupationTypes:BaseEntity
    {
//        CREATE TABLE `tblOccupationTypes` (
//  `OccupationId` int (10) NOT NULL,
//   `OccupationName` varchar(500) NOT NULL,
//   `Status` int (1) NOT NULL
//) ENGINE=MyISAM DEFAULT CHARSET=utf8;

     public string Name { get; set; }
    }
}