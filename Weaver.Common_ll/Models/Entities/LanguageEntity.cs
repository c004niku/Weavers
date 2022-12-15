using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Common.Models.Entities
{
    public class LanguageEntity : BaseEntity
    {
        //        CREATE TABLE `tblLanguage` (
        //  `LanguageId` int (10) NOT NULL,
        //   `LanguageName` varchar(500) NOT NULL,
        //   `AnsiChar` text NOT NULL,
        //  `Status` int (1) NOT NULL
        //) ENGINE=MyISAM DEFAULT CHARSET=utf8;

        public string Name { get; set; }

        public string AnsiChar { get; set; }

        public string Code { get; set; }
        public string LangCode { get; set; }
    }
}