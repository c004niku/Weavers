using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weavers.Models.Sql
{
    public static class UserSqlQueries
    {
        #region Category
        public const string CategoryInsert = "insert into tblCategory(CategoryId, CategoryName, Status)" +
                                              " values('{0}','{1}','{2}')";

        public const string CategoryRetrieve = "select CategoryId, CategoryName, Status, TeluguName, TamilName, HindiName, KannadName from tblCategory";
        #endregion

        #region Language
        public static string LanguageInsert = "insert into tblLanguage(LanguageId, Code, LanguageName, AnsiChar, Status)" +
                                             " values('{0}','{1}','{2}','{3}','{4}')";
        public const string LanguageRetrieve = "select LanguageId, Code, LanguageName, AnsiChar, Status, LangCode from tblLanguage";
        #endregion

        #region tblOccupationTypes
        public static string OccupationTypesInsert = "insert into tblOccupationTypes(OccupationId, OccupationName, Status)" +
                                             " values('{0}','{1}','{2}')";

        public const string OccupationTypesRetrieve = "select OccupationId, OccupationName, Status, HindiName, KannadName, TamilName, TeluguName from tblOccupationTypes Where Status=1";
        #endregion


        #region tblSubCategory
        public static string SubCategoryInsert = "insert into tblSubCategory(SubCategoryId, CategoryId, SubCategoryName, Status)" +
                                             " values('{0}','{1}','{2}','{3}')";

        public const string SubCategoryRetrieveById = "select SubCategoryId, c.CategoryId, SubCategoryName, sc.Status, c.CategoryName, sc.TeluguName, sc.TamilName, sc.HindiName, sc.KannadName from tblSubCategory sc inner join tblCategory c on c.CategoryId=sc.CategoryId where c.CategoryId='{0}' and sc.Status=1";

        public const string SubCategoryRetrieve = "select SubCategoryId, c.CategoryId, SubCategoryName, sc.Status, c.CategoryName, sc.TeluguName, sc.TamilName, sc.HindiName, sc.KannadName  from tblSubCategory sc inner join tblCategory c on c.CategoryId=sc.CategoryId where  sc.Status=1";

        public const string GetCategoryBySubCategory = "SELECT CategoryId FROM tblSubCategory WHERE SubCategoryId='{0}'";

        #endregion

        #region tblUserBasic
        public const string UserBasicInsert = "insert into tblUserBasic(FirstName, LastName, Email, UserName, MobileNumber, LanguageId, UserType, CreatedOn)" +
                                             " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'); SELECT LAST_INSERT_ID();";
        
        public const string UserBasicUpdate = "Update tblUserBasic SET FirstName='{0}', LastName='{1}', LanguageId='{2}', UserType='{3}' " +
                                             " where Id='{4}'";

        public const string UserDelete = "Delete from tblUserBasic where Id='{0}'";

        public const string UserLanguageUpdate = "Update tblUserBasic SET LanguageId='{0}' where Id='{1}'";

        public const string UserEmailUpdate = "Update tblUserBasic SET Email='{0}' where Id='{1}'";


        public const string UserFullNameUpdate = "Update tblUserBasic SET FirstName='{0}', LastName='{1}' where Id='{2}'";


        public const string UserBasicRetrieve = "select Id, FirstName, LastName, Email, UserName, MobileNumber, LanguageId, UserType from tblUserBasic where UserName='{0}'";

        public const string UserBasicRetrieveById = "select Id, FirstName, LastName, Email, UserName, MobileNumber, LanguageId, UserType from tblUserBasic where Id='{0}'";

        public const string UserBasicAllRetrieve = "select Id, FirstName, LastName, Email, UserName, MobileNumber, LanguageId, UserType from tblUserBasic";

        public const string UserExisting = "select count(Id) from tblUserBasic where UserName='{0}' or MobileNumber='{1}'";

        public const string GetExistingUserId = "select Id from tblUserBasic where UserName='{0}' or MobileNumber='{0}'";

        public const string UserExistingByName = "select count(Id) from tblUserBasic where UserName='{0}'";

        public const string UserExistingByMobile = "select count(Id) from tblUserBasic where MobileNumber='{0}'";

        #endregion

        #region tblUserCredential
        public const string UserCredentialInsert = "insert into tblUserCredential(Id, NewPassword, OldPassword, LastLogin, Status)" +
                                            " values('{0}','{1}','{2}','{3}','{4}')";

        public const string UserCredentialRetrieve = "select Id, NewPassword, OldPassword, LastLogin, Status from tblUserCredential";

        public const string DeleteUserCredential = "Delete from tblUserCredential where Id='{0}'";

        public const string UserCredentialCheck = "select Status from tblUserCredential where Id='{0}' and NewPassword='{1}'";

        public const string UpdateUserCredentialId = "Update tblUserCredential set OldPassword=NewPassword, NewPassword={1} where Id='{0}'";

        public const string ChangeUserCredentialId = "Update tblUserCredential set NewPassword='{1}', OldPassword=NewPassword where Id={0} and OldPassword='{2}'";


        #endregion

        #region tblUserProfessionalDetail
        public const string UserProfessionalDetailInsert = "insert into tblUserProfessionalDetail(Id, CategoryId, SubCategoryId, Master_Helper, " +
                                            " ExperienceInYear, Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile, WorkingStatus, WorkingTiming, " +
                                            " ServiceRange, ProfileDescription, ProfileVisibility, Designation, DocumentUrl, OtherCategory, ServiceLocation, GeoLocationLat, GeoLocationLong)" +
                                            " values('{0}','{1}','{2}', '{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')";
       
        public const string UserProfessionalDetailUpdate = "Update tblUserProfessionalDetail SET SubCategoryId='{0}', Master_Helper='{1}', " +
                                             " ExperienceInYear='{2}', BusinessName='{3}', BusinessEmail='{4}', BusinessMobile='{5}', "+
                                             " WorkingStatus='{6}', WorkingTiming='{7}', ServiceRange='{8}', ProfileDescription='{9}', " +
                                               " ProfileVisibility='{10}', CategoryId='{12}', Designation='{13}', DocumentUrl='{14}', OtherCategory='{15}' , ServiceLocation='{16}', Status='{17}', GeoLocationLat='{18}', GeoLocationLong='{19}' " +
                                             " Where Id='{11}'";

        public const string UserProfessionalDetailRetrieve = "SELECT Id, tsc.SubCategoryId,  tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                            "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile,"+
                                            " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, Designation, " +
                                            " DocumentUrl, OtherCategory, ServiceLocation, "+
                                            "  tc.HindiName, tc.TeluguName,tc.KannadName, tc.TamilName, " +
                                            " tsc.HindiName, tsc.TeluguName,tsc.KannadName, tsc.TamilName " +
                                            " FROM tblUserProfessionalDetail tupd " +
                                            " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId "+
                                            "inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where tupd.Id='{0}'";

        public const string UserProfessionalDetailForOther = "SELECT Id, tsc.OccupationId, tsc.OccupationName, tc.CategoryId, tc.CategoryName, " +
                                           "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                           " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, Designation, " +
                                           " DocumentUrl, OtherCategory, ServiceLocation, "+
                                            " tc.HindiName, tc.TeluguName,tc.KannadName, tc.TamilName, " +
                                            " tsc.HindiName, tsc.TeluguName,tsc.KannadName, tsc.TamilName " +
                                            " FROM tblUserProfessionalDetail tupd " +
                                           " inner join tblOccupationTypes tsc on tsc.OccupationId=tupd.SubCategoryId " +
                                           " inner join tblCategory tc on tc.CategoryId=7  " +
                                           " where tupd.Id='{0}'";

        public const string DeleteProfessional = "DELETE from tblUserProfessionalDetail where Id='{0}'";


        public const string UserExperienceUpdate = "Update tblUserProfessionalDetail SET ExperienceInYear='{0}' where Id='{1}'";

        public const string UserWorkingStatusUpdate = "Update tblUserProfessionalDetail SET WorkingStatus='{0}' where Id='{1}'";

        public const string UserServiceRangeUpdate = "Update tblUserProfessionalDetail SET ServiceRange='{0}' where Id='{1}'";

        public const string UserMasterHelperUpdate = "Update tblUserProfessionalDetail SET Master_Helper='{0}' where Id='{1}'";

        public const string UserProfileVisibilityUpdate = "Update tblUserProfessionalDetail SET ProfileVisibility='{0}' where Id='{1}'";

        public const string UserBookmarkUpdate = "Update tblUserProfessionalDetail SET BookMark={0} where Id={1}";

        public const string UserProfessionalDetailRetrieveByCategory = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                           "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                           " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, " +
                                           " tub.MobileNumber, tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, Designation, DocumentUrl, " +
                                           " OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                           " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                           " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                           " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where tc.CategoryId='{0}' and tub.UserType='{1}'";

        public const string UserProfessionalDetailSearchByCategory = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                          "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                          " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, " +
                                          " tub.MobileNumber, tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, Designation," +
                                          " DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                          " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                          " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                          " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where tc.CategoryId='{0}' and tub.UserType='{2}' and " +
                                          " (tsc.SubCategoryName like '%{1}%' or tc.CategoryName  like '%{1}%' or BusinessName  like '%{1}%'  or tupd.ProfileDescription  like '%{1}%')";

        public const string UserProfessionalDetailRetrieveBySubCategory = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                          "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                          " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, "+
                                          " tub.UserName, tub.MobileNumber, tub.CreatedOn, tupd.BookMark, GeoLocationLat, "+
                                          " GeoLocationLong, Designation, DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                          " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                          " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                          " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where tsc.SubCategoryId='{0}' and tub.UserType='{1}'";

        public const string UserProfessionalDetailSearchBySubCategory = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                          "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                          " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, " +
                                          " tub.MobileNumber, tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, " +
                                           " Designation, DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                          " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                          " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                          " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where tsc.SubCategoryId='{0}' and tub.UserType='{2}' and " +
                                          " (tsc.SubCategoryName like '%{1}%' or tc.CategoryName  like '%{1}%' or BusinessName  like '%{1}%'  or tupd.ProfileDescription  like '%{1}%')";

        public const string UserProfessionalDetailSearch = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                         "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                         " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, tub.MobileNumber, "+
                                        " tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, Designation, DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                         " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                         " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                         " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where  tub.UserType='{1}' and " +
                                         " (tsc.SubCategoryName like '%{0}%' or tc.CategoryName  like '%{0}%'  or WorkingTiming  like '%{0}%' or ServiceRange  like '%{0}%' or BusinessName  like '%{0}%'  or tupd.ProfileDescription  like '%{0}%' or tub.FirstName  like '%{0}%' or tub.LastName  like '%{0}%' or tub.Email  like '%{0}%' or MobileNumber  like '%{0}%' )";

        public const string UserProfessionalDetailSearchWithoutType = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                  "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                  " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, tub.MobileNumber, " +
                                 " tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, Designation, DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                  " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                  " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                  " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where " +
                                  " (tsc.SubCategoryName like '%{0}%' or tc.CategoryName  like '%{0}%'  or WorkingTiming  like '%{0}%' or ServiceRange  like '%{0}%' or BusinessName  like '%{0}%'  or tupd.ProfileDescription  like '%{0}%' or tub.FirstName  like '%{0}%' or tub.LastName  like '%{0}%' or tub.Email  like '%{0}%' or MobileNumber  like '%{0}%' )";

        public const string UserProfessionalDetailSearchWithType = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                         "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                         " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, tub.MobileNumber, " +
                                        " tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, Designation, DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                         " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                         " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                         " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where  tub.UserType='{0}' " ;


        public const string UserProfessionalDetail = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                 "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                 " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, " +
                                " tub.MobileNumber, tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, " +
                                " Designation, DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                 " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                 " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                 " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId ";

        public const string UserProfessionalDetailByUserType = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                         "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                         " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, "+
                                        " tub.MobileNumber, tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, " +
                                        " Designation, DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                         " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                         " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                         " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where tub.UserType='{0}'";

        public const string GetUserProfessionalById = "SELECT tupd.Id, tsc.SubCategoryId, tsc.SubCategoryName, tc.CategoryId, tc.CategoryName, " +
                                 "Master_Helper, ExperienceInYear, tupd.Status, Photo, IdProof, BusinessName, BusinessEmail, BusinessMobile," +
                                 " WorkingStatus, WorkingTiming, ServiceRange, tupd.ProfileDescription, tupd.ProfileVisibility, tub.UserName, " +
                                " tub.MobileNumber, tub.CreatedOn, tupd.BookMark, GeoLocationLat, GeoLocationLong, " +
                                " Designation, DocumentUrl, OtherCategory, ServiceLocation, tub.Email,  tub.UserType FROM tblUserProfessionalDetail tupd " +
                                 " inner join tblSubCategory tsc on tsc.SubCategoryId=tupd.SubCategoryId " +
                                 " inner join tblUserBasic tub on tub.Id=tupd.Id" +
                                 " inner join tblCategory tc on tc.CategoryId=tsc.CategoryId where tupd.Id='{0}'";

        #endregion       


        #region tblReportItems

        public const string ReportItemSelect = "SELECT Id, UserId, ReportedOn, ShortDescription, Status,  Action, DetailDescription, ReportType,Images, Url, BookMark FROM tblReportItem";
        
        public const string ReportItemSelectByUser = "SELECT Id, UserId, ReportedOn, ShortDescription, Status,  Action, DetailDescription, ReportType,Images, Url, BookMark FROM tblReportItem WHERE UserId='{0}'";
        
        public const string ReportItemInsert = "insert into tblReportItem(UserId, ReportedOn, ShortDescription, Status,  Action, DetailDescription, ReportType,Images, Url) " +
                                                " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'); SELECT LAST_INSERT_ID();";

        public const string BookMarReportItem = "UPDATE tblReportItem set BookMark={0} where Id={1}";

        public const string DeleteReportItem = "delete from tblReportItem where Id={0} and Status='{1}'";

        #endregion

        #region tblNews

        public const string NewsSelect = "SELECT NewsId, Title, ShortDescription, Image, CreatedBy, tn.CreatedOn, Url, tub.UserName,"+
            " tub.MobileNumber, BookMark, RangeType, tub.Email, tub.UserType  FROM tblNews tn " +
            " inner join tblUserBasic tub on tub.Id=tn.CreatedBy where tub.UserType='{0}'";

        public const string AllNewsSelect = "SELECT NewsId, Title, ShortDescription, Image, CreatedBy, tn.CreatedOn, Url, tub.UserName," +
          " tub.MobileNumber, BookMark, RangeType, tub.Email, tub.UserType  FROM tblNews tn " +
          " inner join tblUserBasic tub on tub.Id=tn.CreatedBy";


        public const string NewsInsert = "INSERT INTO tblNews(Title, ShortDescription, Image, CreatedBy, CreatedOn, Url, RangeType) " +
            " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}'); SELECT LAST_INSERT_ID();";

        public const string NewsDelete = "Delete from tblNews where NewsId='{0}'";

        public const string BookMarkNews = "UPDATE tblNews set BookMark={0} where NewsId={1}";

        #endregion

        #region tblAlerts

        public const string AlertSelect = "SELECT AlertId, Title, ShortDescription, Image, CreatedBy, ta.CreatedOn, Url, tub.UserName, " +
                " tub.MobileNumber, BookMark, RangeType, tub.Email, tub.UserType  FROM tblAlerts ta" +
                " inner join tblUserBasic tub on tub.Id=ta.CreatedBy where tub.UserType='{0}'";


        public const string AlertInsert = "INSERT INTO tblAlerts(Title, ShortDescription, Image, CreatedBy, CreatedOn, Url, RangeType) " +
            " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}'); SELECT LAST_INSERT_ID();";

        public const string BookMarkAlert = "UPDATE tblAlerts set BookMark={0} where AlertId={1}";

        #endregion

        #region tblVideo

        public const string VideoSelect = "SELECT VideoId, Title, ShortDescription, CreatedBy, tv.CreatedOn, Url, " +
             " tub.UserName, tub.MobileNumber, Image, BookMark, tub.Email, tub.UserType  FROM tblVideo tv " +
             " inner join tblUserBasic tub on tub.Id=tv.CreatedBy where tub.UserType='{0}'";


        public const string VideoInsert = "INSERT INTO tblVideo(Title, ShortDescription, CreatedBy, CreatedOn, Url, Image) " +
            " VALUES ('{0}','{1}','{2}','{3}','{4}','{5}'); SELECT LAST_INSERT_ID();";

        public const string BookMarkVideo = "UPDATE tblVideo set BookMark={0} where VideoId={1}";

        #endregion

        #region searchQuery
        public const string SearchNewsQuery = "SELECT NewsId, Title, ShortDescription, Image, CreatedBy, tn.CreatedOn, Url, tub.UserName, tub.MobileNumber, BookMark FROM tblNews tn " +
            " inner join tblUserBasic tub on tub.Id=tn.CreatedBy " +
             "  where tub.UserType='{1}' and Title like '%{0}%'";

        public const string SearchVideoQuery = "SELECT VideoId, Title, ShortDescription, Image, CreatedBy, tv.CreatedOn, Url, tub.UserName, tub.MobileNumber, BookMark  FROM tblVideo tv " +
             " inner join tblUserBasic tub on tub.Id=tv.CreatedBy " +
             "  where tub.UserType='{1}' and Title like '%{0}%'";

        public const string SearchAlertQuery = "SELECT AlertId, Title, ShortDescription, Image, CreatedBy, ta.CreatedOn, Url, tub.UserName, tub.MobileNumber, BookMark  FROM tblAlerts ta" +
             " inner join tblUserBasic tub on tub.Id=ta.CreatedBy " +
             "  where tub.UserType='{1}' and Title like '%{0}%'";

        public const string SearchReportQuery = "SELECT tr.Id, ShortDescription, DetailDescription, Images, UserId, ReportedOn, Url, tub.UserName, tub.MobileNumber, BookMark, Status,  Action, ReportType FROM tblReportItem tr " +
            " inner join tblUserBasic tub on tub.Id=tr.UserId " +
            "  where tub.UserType='{1}' and (ShortDescription like '%{0}%' or  DetailDescription like '%{0}%')";


        #endregion

        #region bookmarkDetail
        
        public const string UserItemBookMarkSelect = "SELECT BookMarkId,  BookMarkType, BookMarkItemId, UserId, IsBookMarked FROM  UserItemBookMark  where UserId={0} and IsBookMarked='1' ";

        public const string UserItemBookMarkInsert = "INSERT into UserItemBookMark(BookMarkType, BookMarkItemId, UserId, IsBookMarked) "+
            "  VALUES ('{0}','{1}','{2}','{3}'); SELECT LAST_INSERT_ID(); ";

        public const string UserItemBookMarkUpdate = "UPDATE UserItemBookMark set IsBookMarked='{0}' where BookMarkId={1}";

        public const string BookMarkCount = "SELECT BookMarkType, count(BookMarkId) FROM  UserItemBookMark where UserId = '{0}' and IsBookMarked = '1' group by BookMarkType";

        #endregion
    }
}