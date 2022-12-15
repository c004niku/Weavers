using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Weavers.Common.Models;
using Weavers.Common.Models.Entities;
using Weavers.Data;

using Weavers.Models.Helpers;
using Weavers.Models.Sql;

namespace Weavers.Models.DbHelper
{
    public class tblUserBasic
    {

        public static ICollection<UserBasicEntity> Get(string userName="")
        {
            var entities = new Collection<UserBasicEntity>();

            var query = string.IsNullOrEmpty(userName) ?  UserSqlQueries.UserBasicRetrieve : string.Format(UserSqlQueries.UserBasicRetrieve, userName);
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                entities.Add(new UserBasicEntity
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    FullName = reader[1].ToString(),
                    //LastName = reader[2].ToString(),
                    Email = reader[3].ToString(),
                    UserName = reader[4].ToString(),
                    MobileNumber = reader[5].ToString(),
                    LangId = Convert.ToInt32(reader[6].ToString()),
                    UserType = reader[7].ToString()
                });
            }

            return entities;
        }

        public static ICollection<UserBasicEntity> Get(int userId)
        {
            var entities = new Collection<UserBasicEntity>();

            var query = userId==0? UserSqlQueries.UserBasicAllRetrieve : string.Format(UserSqlQueries.UserBasicRetrieveById, userId);
            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                entities.Add(new UserBasicEntity
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    FullName = reader[1].ToString(),
                    //LastName = reader[2].ToString(),
                    Email = reader[3].ToString(),
                    UserName = reader[4].ToString(),
                    MobileNumber = reader[5].ToString(),
                    LangId = Convert.ToInt32(reader[6].ToString()),
                    UserType = reader[7].ToString()
                });
            }

            return entities;
        }


        public static UserBasicEntity Save(UserBasicEntity userBasicEntity)
        {
            var query = string.Format(UserSqlQueries.UserBasicInsert, userBasicEntity.FullName, "",
                                    userBasicEntity.Email, userBasicEntity.UserName, userBasicEntity.MobileNumber, userBasicEntity.LangId, userBasicEntity.UserType, DateTime.Now.ToString("yyyy-MM-dd"));
            
            var userResult=SaveData.SaveAndGetId(query);
            
            userBasicEntity.ID = userResult;

            return userBasicEntity;
        }

        public static UserBasicEntity UpdateUser(UserBasicEntity userBasicEntity)
        {
            var query = string.Format(UserSqlQueries.UserBasicUpdate, userBasicEntity.FullName, "",
                                    userBasicEntity.LangId, userBasicEntity.UserType, userBasicEntity.ID);

            var userResult = SaveData.Save(query);

            return userBasicEntity;
        }

        public static bool UpdateUserByValue(UserUpdateType userBasicEntity)
        {
            var query = "";// string.Format(UserSqlQueries.UserLanguageUpdate, Convert.ToInt32(userBasicEntity.UpdateValue), userBasicEntity.userId);

            switch (userBasicEntity.UpdateType)
            {
                case EnumUpdateType.Language:
                    query = string.Format(UserSqlQueries.UserLanguageUpdate, Convert.ToInt32(userBasicEntity.UpdateValue), userBasicEntity.userId);
                    break;
                case EnumUpdateType.Email:
                    query = string.Format(UserSqlQueries.UserEmailUpdate, userBasicEntity.UpdateValue, userBasicEntity.userId);
                    break;
                case EnumUpdateType.Experience:
                    query = string.Format(UserSqlQueries.UserExperienceUpdate, Convert.ToInt32(userBasicEntity.UpdateValue), userBasicEntity.userId);
                    break;
                case EnumUpdateType.IsWorking:
                    query = string.Format(UserSqlQueries.UserWorkingStatusUpdate, Convert.ToBoolean(userBasicEntity.UpdateValue)?1:0, userBasicEntity.userId);
                    break;
                case EnumUpdateType.ServiceArea:
                    query = string.Format(UserSqlQueries.UserServiceRangeUpdate, userBasicEntity.UpdateValue, userBasicEntity.userId);
                    break;
                case EnumUpdateType.IsHelper:
                    query = string.Format(UserSqlQueries.UserMasterHelperUpdate, userBasicEntity.UpdateValue, userBasicEntity.userId);
                    break;
                case EnumUpdateType.ProfileVisibility:
                    query = string.Format(UserSqlQueries.UserProfileVisibilityUpdate, Convert.ToBoolean(userBasicEntity.UpdateValue)?"Y":"N", userBasicEntity.userId);
                    break;
                default:
                    break;
            }
            

            var userResult = !string.IsNullOrEmpty(userBasicEntity.UpdateValue) ? SaveData.Save(query):0;

            return userResult>0;
        }

        public static UserUpdateFullName UpdateUserFullName(UserUpdateFullName userBasicEntity)
        {
            var query = string.Format(UserSqlQueries.UserFullNameUpdate, userBasicEntity.FirstName, userBasicEntity.LastName,
                                     userBasicEntity.UserId);

            var userResult = SaveData.Save(query);

            return userBasicEntity;
        }

        public static UserProfessionalDetail SaveUserProfessional(UserProfessionalDetail userProfessional, string fullname, string mobileNumber="")
        {
            int categoryId = 0;
            try
            {
                var categoryquery = string.Format(UserSqlQueries.GetCategoryBySubCategory, userProfessional.OccupationId);

                var categoryResult = GetData.IsRecordExisting(categoryquery);
                categoryId = Convert.ToInt32(categoryResult);
            }
            catch (Exception ex)
            {

            }

            if (userProfessional.Designation == null)
                userProfessional.Designation = "";

            if (userProfessional.DocumentUrl == null)
                userProfessional.DocumentUrl = "";

            if (userProfessional.OtherOccupation == null)
                userProfessional.OtherOccupation = "";

            if (!string.IsNullOrEmpty(userProfessional.ServiceLocation))
            {
                GetDistanceResponseModel webResult = null;

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://api.positionstack.com/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // HTTP GET
                        var response = client.GetAsync($"http://api.positionstack.com/v1/forward?access_key=4cf0d33ba677c7deeda4970d22c89ad9&query={(string.IsNullOrEmpty(userProfessional.ServiceLocation)?"Hyderabad":userProfessional.ServiceLocation)}");
                        response.Wait();
                        if (response.Result.IsSuccessStatusCode)
                        {
                            var reslt = response.Result.Content.ReadAsStringAsync().Result;
                            webResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GetDistanceResponseModel>(reslt);
                            userProfessional.ServiceLatitude = webResult.data.FirstOrDefault().latitude.ToString();
                            userProfessional.ServiceLongitude = webResult.data.FirstOrDefault().longitude.ToString();
                        }

                    }
                }
                catch (Exception ex)
                {
                    
                }
            }

            var query = string.Format(UserSqlQueries.UserProfessionalDetailInsert, userProfessional.ID, categoryId, userProfessional.OccupationId,
                                    userProfessional.Master_Helper, (userProfessional.WorkingSince.ToString().Length==4?(DateTime.Now.Year-userProfessional.WorkingSince):userProfessional.WorkingSince), userProfessional.Status, "Photo",
                                    "IdProof", fullname, userProfessional.BusinessEmail, userProfessional.BusinessMobile,
                                    userProfessional.WorkingStatus?1:0, userProfessional.WorkingTiming,
                                    userProfessional.ServiceRange, userProfessional.ProfileDescription, 
                                    "Y", 
                                    userProfessional.Designation,
                                    userProfessional.DocumentUrl,
                                    userProfessional.OtherOccupation, userProfessional.ServiceLocation, userProfessional.ServiceLatitude, userProfessional.ServiceLongitude);

            var userResult = SaveData.Save(query);
            var imageFolderPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");
            Task imageUploadTask=new Task(() =>UploadImageTask(userProfessional, mobileNumber, imageFolderPath));

            imageUploadTask.Start();

            return userProfessional;
        }

        /// <summary>
        /// Update user Professional Detail
        /// </summary>
        /// <param name="userProfessional"></param>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static UserProfessionalDetail UpdateUserProfessional(UserProfessionalDetail userProfessional, UserBasicEntity userBasicEntity, string mobileNumber = "")
        {

            int categoryId = 0;
            try
            {
                var categoryquery = string.Format(UserSqlQueries.GetCategoryBySubCategory, userProfessional.OccupationId);

                var categoryResult = GetData.IsRecordExisting(categoryquery);
                categoryId = Convert.ToInt32(categoryResult);
            }
            catch (Exception ex)
            {

            }

            if (!string.IsNullOrEmpty(userProfessional.ServiceLocation))
            {
                GetDistanceResponseModel webResult = null;

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://api.positionstack.com/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // HTTP GET
                        var response = client.GetAsync($"http://api.positionstack.com/v1/forward?access_key=4cf0d33ba677c7deeda4970d22c89ad9&query={(string.IsNullOrEmpty(userProfessional.ServiceLocation) ? "Hyderabad" : userProfessional.ServiceLocation)}");
                        response.Wait();
                        if (response.Result.IsSuccessStatusCode)
                        {
                            var reslt = response.Result.Content.ReadAsStringAsync().Result;
                            webResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GetDistanceResponseModel>(reslt);
                            userProfessional.ServiceLatitude = webResult.data.FirstOrDefault().latitude.ToString();
                            userProfessional.ServiceLongitude = webResult.data.FirstOrDefault().longitude.ToString();
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            }

            var query = string.Format(UserSqlQueries.UserProfessionalDetailUpdate, userProfessional.OccupationId,
                                    userProfessional.Master_Helper, userProfessional.WorkingSince, userBasicEntity.FullName, userProfessional.BusinessEmail, userProfessional.BusinessMobile,
                                    userProfessional.WorkingStatus ? 1 : 0, userProfessional.WorkingTiming, 
                                    userProfessional.ServiceRange, userProfessional.ProfileDescription,
                                    userProfessional.ProfileVisibility?"Y":"N", userProfessional.ID, categoryId, 
                                    userProfessional.Designation, userProfessional.DocumentUrl, userProfessional.OtherOccupation, 
                                    userProfessional.ServiceLocation, userProfessional.Status, userProfessional.ServiceLatitude, userProfessional.ServiceLongitude);

            var userResult = SaveData.Save(query);
            
            var imageFolderPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");
            Task imageUploadTask = new Task(() => UploadImageTask(userProfessional, mobileNumber, imageFolderPath));

            imageUploadTask.Start();
            var getUserProfessionals = GetUserProfessional(userBasicEntity.ID, userBasicEntity.UserType, userBasicEntity.LangId);
            return getUserProfessionals.FirstOrDefault(); ;
        }

        private static void UploadImageTask(UserProfessionalDetail userProfessional, string mobileNumber, string imageFolderPath)
        {
            var profileImage=  UploadImage.SaveImage(userProfessional.Photo, mobileNumber, imageFolderPath+ "\\User\\Profile", userProfessional.ID.ToString());
           
            var idProofImage=UploadImage.SaveImage(userProfessional.IdProof, mobileNumber, imageFolderPath+"\\User\\IdProof", userProfessional.ID.ToString());
        }

        public static void UpdateImage(UpdateImageEntity updateImage)
        {
            var imageFolderPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");
            UploadImage.SaveImage(updateImage.image, updateImage.userProfileMobile, imageFolderPath + (updateImage.isProfile ? "\\User\\Profile": "\\User\\IdProof"), updateImage.userId.ToString());
        }

        public static IEnumerable<UserProfessionalDetail> GetUserProfessional(int userId, string userType, int langId)
        {
            var entities = new Collection<UserProfessionalDetail>();

            var query = string.Format(UserSqlQueries.UserProfessionalDetailRetrieve, userId);
            if (userType.ToLower() == "others")
            {
                query = string.Format(UserSqlQueries.UserProfessionalDetailForOther, userId);
            }

            var dataSet = GetData.SelectData(query);

            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                var categoryName = reader[4].ToString();
                switch (langId)
                {
                    case 2:
                        categoryName = reader[22].ToString();
                        break;
                    case 3:
                        categoryName = reader[23].ToString();
                        break;
                    case 4:
                        categoryName = reader[24].ToString();
                        break;
                    case 5:
                        categoryName = reader[25].ToString();
                        break;
                    default:
                        categoryName = reader[4].ToString();
                        break;
                }


                var subcategoryName = reader[2].ToString();
                switch (langId)
                {
                    case 2:
                        subcategoryName = reader[26].ToString();
                        break;
                    case 3:
                        subcategoryName = reader[27].ToString();
                        break;
                    case 4:
                        subcategoryName = reader[28].ToString();
                        break;
                    case 5:
                        subcategoryName = reader[29].ToString();
                        break;
                    default:
                        subcategoryName = reader[2].ToString();
                        break;
                }

                entities.Add(new UserProfessionalDetail
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Category = new CodeValueEntity { Id = Convert.ToInt32(reader[3].ToString()), Name= reader[4].ToString(), NameToDisplay = categoryName },
                    SubCategory = new CodeValueEntity { Id = Convert.ToInt32(reader[1].ToString()), Name = reader[2].ToString(), NameToDisplay= subcategoryName },
                    OccupationId= Convert.ToInt32(reader[1].ToString()),
                    Master_Helper = reader[5].ToString(),
                    WorkingSince = (DateTime.Now.Year - Convert.ToInt32(reader[6].ToString())),
                    Status = reader[7].ToString(),
                    Photo = reader[8].ToString(),
                    IdProof = reader[9].ToString(),
                    //BusinessName = reader[10].ToString(),
                    BusinessEmail = reader[11].ToString(),
                    BusinessMobile = reader[12].ToString(),
                    WorkingStatus= reader[13].ToString()=="1",
                    WorkingTiming = reader[14].ToString(),
                    ServiceRange = reader[15].ToString(),
                    ProfileDescription = reader[16].ToString(),
                    ProfileVisibility = !reader.IsDBNull(17) && reader[17].ToString()=="Y",
                    Designation = reader[18].ToString(),
                    DocumentUrl = reader[19].ToString(),
                    OtherOccupation = reader[20].ToString(),
                    ServiceLocation = reader[21].ToString()
                });
            }

            return entities;
        }

        public static UserCredentialEntity SaveUserCredential(UserCredentialEntity user)
        {
            var query = string.Format(UserSqlQueries.UserCredentialInsert, user.ID, user.NewPassword, user.OldPassword, user.LastLogin, 1);

            var userResult = SaveData.Save(query);

            return user;
        }

        public static UserEntity CheckUserCredential(UserLoginEntity user)
        {
            UserEntity userEntity = null;
            var userEntitis = Get(user.UserName);
            if(userEntitis!=null)
            {
                var userResponse = userEntitis.FirstOrDefault();
                if (userResponse != null)
                {
                    var query = string.Format(UserSqlQueries.UserCredentialCheck, userResponse.ID, user.Password);

                    var userResult = GetData.IsRecordExisting(query);
                    if (Convert.ToInt32(userResult) == 1)
                    {
                        userEntity = new UserEntity { userBasicEntity = userResponse };

                        var professionDetail = GetUserProfessional(userResponse.ID, userResponse.UserType, userResponse.LangId);

                        if (professionDetail != null)
                        {
                            userEntity.userProfessionalDetail = professionDetail.FirstOrDefault();
                            userEntity.userProfessionalDetail.Photo = GlobalConstants.GetProfileImagePath(userEntity.userBasicEntity.MobileNumber, userEntity.userBasicEntity.ID.ToString());
                            userEntity.userProfessionalDetail.IdProof = GlobalConstants.GetIdProofImagePath(userEntity.userBasicEntity.MobileNumber, userEntity.userBasicEntity.ID.ToString());
                        }

                        return userEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
            }            

            return null;
        }

        public static UserEntity GetUserEntity(int userId)
        {
            var uerBasicEntity = Get(userId)?.FirstOrDefault();
           var userEntity = new UserEntity { userBasicEntity = uerBasicEntity };

            var professionDetail = GetUserProfessional(uerBasicEntity.ID, uerBasicEntity.UserType, uerBasicEntity.LangId);

            if (professionDetail != null)
            {
                userEntity.userProfessionalDetail = professionDetail.FirstOrDefault();
                userEntity.userProfessionalDetail.Photo = GlobalConstants.GetProfileImagePath(userEntity.userBasicEntity.MobileNumber, userEntity.userBasicEntity.ID.ToString());
                userEntity.userProfessionalDetail.IdProof = GlobalConstants.GetIdProofImagePath(userEntity.userBasicEntity.MobileNumber, userEntity.userBasicEntity.ID.ToString());
            }
            return userEntity;
        }

        public static ICollection<UserEntity> GetAllUserEntity()
        {
            var userEntities = new Collection<UserEntity>();
            var uerBasicEntities = Get(0);
            foreach (var uerBasicEntity in uerBasicEntities)
            {
                var userEntity = new UserEntity { userBasicEntity = uerBasicEntity };

                var professionDetail = GetUserProfessional(uerBasicEntity.ID, uerBasicEntity.UserType, uerBasicEntity.LangId);

                if (professionDetail != null)
                {
                    userEntity.userProfessionalDetail = professionDetail.FirstOrDefault();
                    userEntity.userProfessionalDetail.Photo = GlobalConstants.GetProfileImagePath(userEntity.userBasicEntity.MobileNumber, userEntity.userBasicEntity.ID.ToString());
                    userEntity.userProfessionalDetail.IdProof = GlobalConstants.GetIdProofImagePath(userEntity.userBasicEntity.MobileNumber, userEntity.userBasicEntity.ID.ToString());
                }
                
                userEntities.Add(userEntity);

            }
            
            return userEntities;
        }

        public static bool CheckIfUserExisting(UserBasicEntity user)
        {
            bool existingUser=false;
            var query = string.Format(UserSqlQueries.UserExisting, user.UserName, user.MobileNumber);

            var userResult = GetData.IsRecordExisting(query);
            existingUser = Convert.ToInt32(userResult) > 0;
            

            return existingUser;
        }

        public static int GetUserId(string userOrMobile)
        {
            var query = string.Format(UserSqlQueries.GetExistingUserId, userOrMobile);

            var userResult = GetData.IsRecordExisting(query);
            return Convert.ToInt32(userResult);
        }

        public static bool UpdateExistingUserPassword(ForgotPasswordEntity user)
        {           
            var query = string.Format(UserSqlQueries.UpdateUserCredentialId, user.ID, user.NewPassword);

            var userResult = SaveData.Save(query);
            
           return Convert.ToInt32(userResult)>0;
        }

        public static bool ChangeExistingUserPassword(ChangePasswordEntity user)
        {
            var query = string.Format(UserSqlQueries.ChangeUserCredentialId, user.ID, user.NewPassword,user.OldPassword);

            var userResult = SaveData.Save(query);

            return Convert.ToInt32(userResult) > 0;
        }

        public static bool CheckIfUserExistingByName(string userName)
        {
            bool existingUser = false;
            var query = string.Format(UserSqlQueries.UserExistingByName, userName);

            var userResult = GetData.IsRecordExisting(query);
            existingUser = Convert.ToInt32(userResult) > 0;


            return existingUser;
        }

        public static bool CheckIfUserExistingByMobile(string mobileNumber)
        {
            bool existingUser = false;
            var query = string.Format(UserSqlQueries.UserExistingByMobile, mobileNumber);

            var userResult = GetData.IsRecordExisting(query);
            existingUser = Convert.ToInt32(userResult) > 0;


            return existingUser;
        }

        public static void DeleteUser(UserBasicEntity user)
        {
            var deletequery = string.Format(UserSqlQueries.UserDelete, user.ID);

            var userResult = SaveData.Save(deletequery);

            deletequery = string.Format(UserSqlQueries.DeleteUserCredential, user.ID);

            userResult = SaveData.Save(deletequery);

            deletequery = string.Format(UserSqlQueries.DeleteProfessional, user.ID);

            userResult = SaveData.Save(deletequery);

        }

        public static IEnumerable<UserShops> GetUsersByCategory(int categoryId, string searchText="", string userType = null)
        {
            var entities = new Collection<UserShops>();

            var query = string.Format(UserSqlQueries.UserProfessionalDetailRetrieveByCategory, categoryId, userType);
            if (!string.IsNullOrEmpty(searchText))
            {
                query = string.Format(UserSqlQueries.UserProfessionalDetailSearchByCategory, categoryId, searchText, userType);
            }

            var dataSet = GetData.SelectData(query);

            GetUsersEntitis(entities, dataSet);

            return entities;
        }

        public static IEnumerable<UserShops> GetUsersBySubCategory(int subcategoryId, string searchText, string userType )
        {
            var entities = new Collection<UserShops>();

            var query = string.Format(UserSqlQueries.UserProfessionalDetailRetrieveBySubCategory, subcategoryId, userType);
            if (!string.IsNullOrEmpty(searchText))
            {
                query = string.Format(UserSqlQueries.UserProfessionalDetailSearchBySubCategory, subcategoryId, searchText,userType);
            }

            var dataSet = GetData.SelectData(query);

            GetUsersEntitis(entities, dataSet);

            return entities;
        }

        public static IEnumerable<UserShops> SearchUsers(string searchText="", string userType="")
        {
            var entities = new Collection<UserShops>();

            var query = string.Format(UserSqlQueries.UserProfessionalDetail);

            if (!string.IsNullOrEmpty(searchText) && string.IsNullOrEmpty(userType))
            {
                query = string.Format(UserSqlQueries.UserProfessionalDetailSearchWithoutType, searchText);
            }

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(userType))
            {
                query = string.Format(UserSqlQueries.UserProfessionalDetailSearch, searchText,userType);
            }

            if (string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(userType))
            {
                query = string.Format(UserSqlQueries.UserProfessionalDetailSearchWithType, userType);
            }

            var dataSet = GetData.SelectData(query);

            GetUsersEntitis(entities, dataSet);

            return entities.Where(s=>s.ProfileVisibility);
        }

        public static UserShops GetUserFullDetail(string userId)
        {
            var entities = new Collection<UserShops>();

            var query = string.Format(UserSqlQueries.GetUserProfessionalById, userId);
           

            var dataSet = GetData.SelectData(query);

            GetUsersEntitis(entities, dataSet);

            return entities.FirstOrDefault();
        }


        private static void GetUsersEntitis(Collection<UserShops> entities, System.Data.DataSet dataSet)
        {
            var reader = dataSet.CreateDataReader();

            while (reader.Read())
            {
                var memberSince = 0;
                Int32.TryParse(reader[6].ToString(), out memberSince);
                entities.Add(new UserShops
                {
                    ID = Convert.ToInt32(reader[0].ToString()),
                    Category = new CodeValueEntity { Id = Convert.ToInt32(reader[3].ToString()), Name = reader[4].ToString() },
                    SubCategory = new CodeValueEntity { Id = Convert.ToInt32(reader[1].ToString()), Name = reader[2].ToString() },
                    OccupationId = Convert.ToInt32(reader[1].ToString()),
                    Master_Helper = reader[5].ToString(),
                    WorkingSince = (DateTime.Now.Year - memberSince),
                    Status = reader[7].ToString(),
                    Photo = GlobalConstants.GetProfileImagePath(reader[19].ToString(), reader[0].ToString()),
                    IdProof = GlobalConstants.GetIdProofImagePath(reader[19].ToString(), reader[0].ToString()),
                    //BusinessName = reader[10].ToString(),
                    BusinessEmail = reader[11].ToString(),
                    BusinessMobile = reader[12].ToString(),
                    WorkingStatus = reader[13].ToString() == "1",
                    WorkingTiming = reader[14].ToString(),
                    ServiceRange = reader[15].ToString(),
                    ProfileDescription = reader[16].ToString(),
                    ProfileVisibility = !reader.IsDBNull(17) && reader[17].ToString() == "Y",
                    User = new CodeEntity
                    {
                        Name = reader[18].ToString(),
                        Id = Convert.ToInt32(reader[0].ToString()),
                        ProfileImage = GlobalConstants.GetProfileImagePath(reader[19].ToString(), reader[0].ToString()),
                        MobileNumber = reader[19].ToString(),
                        Email=reader[28].ToString(),
                        UserType=reader[29].ToString(),
                    },
                    ShopName = reader[10].ToString(),
                    ShopImages = new Collection<string>() { GlobalConstants.GetProfileImagePath(reader[19].ToString(), reader[0].ToString()) },
                    MobileNumber = reader[19].ToString(),
                    MemberSince = Convert.ToDateTime(reader[20].ToString()),
                    IsBookMarked = !reader.IsDBNull(21) && Convert.ToInt32(reader[21]) == 1,
                    GeoLocation = new GeoLocation { Latitude = reader[22].ToString(), Longitude = reader[23].ToString() },
                    Designation=reader[24].ToString(),
                    DocumentUrl=reader[25].ToString(),
                    OtherOccupation=reader[26].ToString(),
                    ServiceLocation=reader[27].ToString()
                });
            }
        }
    }
}