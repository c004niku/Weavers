using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class UserProfessionalDetail:BaseEntity
    {
//        CREATE TABLE `tblUserProfessionalDetail` (
//  `Id` int (10) NOT NULL,
//   `CategoryId` int (10) NOT NULL,
//    `SubCategoryId` int (10) NOT NULL,
//     `Master_Helper` varchar(30) NOT NULL,
//     `ExperienceInYear` int (3) NOT NULL,
//      `Status` int (1) NOT NULL,
//       `Photo` varchar(500) NOT NULL,
//       `IdProof` varchar(500) NOT NULL,
//       `BusinessName` varchar(500) NOT NULL,
//       `BusinessEmail` varchar(500) NOT NULL,
//       `BusinessMobile` varchar(14) NOT NULL
//) ENGINE=MyISAM DEFAULT CHARSET=utf8;
    
        public CodeValueEntity Category { get; set; }
        public CodeValueEntity SubCategory { get; set; }
        public string Master_Helper { get; set; }

        public int OccupationId { get; set; }

        public string OtherOccupation { get; set; }

        //public int ExperienceInYear { get; set; }
        public int WorkingSince { get; set; }

        public string Photo { get; set; }
        public string IdProof { get; set; }
        //public string BusinessName { get; set; }
        public string BusinessEmail { get; set; }
        public string BusinessMobile { get; set; }

        public bool WorkingStatus { get; set; }
        public new string Status { get; set; }

        public string WorkingTiming { get; set; }
        public string ServiceRange { get; set; }

        public string ProfileDescription { get; set; }

        public bool ProfileVisibility { get; set; }

        public string Designation { get; set; }

        public string DocumentUrl { get; set; }

        public string ServiceLocation { get; set; }
        public string ServiceLatitude { get; set; }
        public string ServiceLongitude { get; set; }

    }

    public class UpdateImageEntity
    {
        public bool isProfile { get; set; }

        public string userProfileMobile { get; set; }

        public int userId { get; set; }

        public string image { get; set; }
    }

    public class UserShops : UserProfessionalDetail
    {
        public CodeEntity User { get; set; }

        public DateTime MemberSince { get; set; }
        public string ShopName { get; set; }

        public string MobileNumber { get; set; }
        public Collection<string> ShopImages { get; set; }

        public GeoLocation GeoLocation { get; set; }
        public bool IsBookMarked { get; set; }

        public string ServiceLocation { get; set; }

        public double Distance { get; set; }
    }

    public class GeoLocation
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}