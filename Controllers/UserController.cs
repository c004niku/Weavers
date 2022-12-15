using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Common.Models.Entities;
using Weavers.Models.DbHelper;


namespace Weavers.Controllers
{
    public class UserController : ApiController
    {


        public IEnumerable<CategoryEntity> Get()
        {
            return new CategoryEntity[] { new CategoryEntity { Name = "Master" } };
        }

        [Route("api/User/GetOtp")]
        [HttpGet]
        public CustomResponse GetOtpForPassword(string mobileNumber)
        {
            var customResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {

                var existinguser = tblUserBasic.GetUserId(mobileNumber);
                if (existinguser==0)
                {
                    customResponse = new CustomResponse { status = HttpStatusCode.NotFound, message = "User not available" };
                }
                else
                {
                    customResponse.status = HttpStatusCode.OK;
                    customResponse.message = string.Format("Otp sent on your mobile");
                }
            }
            catch (Exception ex)
            {
                customResponse = new CustomResponse { status = HttpStatusCode.Found, message = ex.Message };
            }
            return customResponse;
        }

        [Route("api/User/ForgetPassword")]
        [HttpPost]
        public CustomResponse UpdatePassword(ForgotPasswordEntity user)
        {
            var customResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {

                user.ID= tblUserBasic.GetUserId(user.MobileNumber);
                
                
                if (user.NewPassword != user.ConfirmPassword)
                {
                    customResponse = new CustomResponse { status = HttpStatusCode.NotAcceptable, message = "New Password and Confirm Password doesn't match" };
                }
                else
                {
                    if (user.ID>0)
                    {
                        var existinguser = tblUserBasic.UpdateExistingUserPassword(user);
                        customResponse.status = existinguser?HttpStatusCode.OK: HttpStatusCode.ExpectationFailed;
                        customResponse.message = $"Your password changed successfully";
                    }
                    else
                    {
                        customResponse = new CustomResponse { status = HttpStatusCode.Found, message = "User not exist" };
                    }
                }
            }
            catch (Exception ex)
            {
                customResponse = new CustomResponse { status = HttpStatusCode.Found, message = ex.Message };
            }
            return customResponse;
        }

        [Route("api/User/ChangePassword")]
        [HttpPost]
        public CustomResponse ChangePassword(ChangePasswordEntity user)
        {
            var customResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                user.ID= tblUserBasic.GetUserId(user.MobileNumber);

                
                if (user.NewPassword != user.ConfirmPassword)
                {
                    customResponse = new CustomResponse { status = HttpStatusCode.NotAcceptable, message = "New Password and Confirm Password doesn't match" };
                }
                else
                {
                    if (user.ID > 0)
                    {
                        var existinguser = tblUserBasic.ChangeExistingUserPassword(user);
                        customResponse.status = existinguser ? HttpStatusCode.OK : HttpStatusCode.ExpectationFailed;
                        customResponse.message = existinguser ? "Your password changed successfully":"Your password can't be changed as Old Password is not correct";
                    }
                    else
                    {
                        customResponse = new CustomResponse { status = HttpStatusCode.Found, message = "User not exist" };
                    }
                }
            }
            catch (Exception ex)
            {
                customResponse = new CustomResponse { status = HttpStatusCode.Found, message = ex.Message };
            }
            return customResponse;
        }

        [Route("api/User/Update")]
        [HttpPost]
        public CustomResponse UpdateUserProfile(UserUpdateAccount requestObject)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (requestObject.userBasicEntity == null || string.IsNullOrEmpty(requestObject.userBasicEntity.UserName) || requestObject.userBasicEntity.ID==0)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "Profile update failed, not a valid user" };
                    return apiResponse;
                }
                
                var userBasicEntity = tblUserBasic.UpdateUser(requestObject.userBasicEntity);

                requestObject.userProfessionalDetail.ID = userBasicEntity.ID;
                                
                var userProfessionalDetail = tblUserBasic.UpdateUserProfessional(requestObject.userProfessionalDetail, userBasicEntity, requestObject.userBasicEntity.MobileNumber);
                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "User Updated Successfully", data=requestObject };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "Profile update missing filedName, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }

        [Route("api/User/UpdateUserByValue")]
        [HttpPost]
        public CustomResponse UpdateUserByValue(UserUpdateType requestObject)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (requestObject.UpdateType == null || string.IsNullOrEmpty(requestObject.UserMobile))
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "not a valid user" };
                    return apiResponse;
                }

                requestObject.userId = tblUserBasic.GetUserId(requestObject.UserMobile);

                if (requestObject.userId > 0)
                {
                    var userBasicEntity = tblUserBasic.UpdateUserByValue(requestObject);

                    var userEnttiy = tblUserBasic.GetUserEntity(requestObject.userId);

                    apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "User Updated Successfully", data = userEnttiy };
                }
                else
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "user not exist" };
                }
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "update missing filedName, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }

        [Route("api/User/Delete")]
        [HttpPost]
        public CustomResponse DeleteUserAccount(UserUpdateAccount requestObject)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (requestObject.userBasicEntity == null || string.IsNullOrEmpty(requestObject.userBasicEntity.UserName) || requestObject.userBasicEntity.ID == 0)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "Not a valid user" };
                    return apiResponse;
                }

                var existingUser = tblUserBasic.CheckIfUserExisting(requestObject.userBasicEntity);

                if (existingUser)
                {
                    tblUserBasic.DeleteUser(requestObject.userBasicEntity);
                }
                
                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "User account deleted Successfully", data = requestObject };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "account delete failed, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }


        [Route("api/User/UpdateProfileImage")]
        [HttpPost]
        public CustomResponse UpdateProfileImage(UpdateImageEntity requestObject)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (string.IsNullOrEmpty(requestObject.userProfileMobile))
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "Not a valid user" };
                    return apiResponse;
                }

                if (string.IsNullOrEmpty(requestObject.image) || requestObject.image.Length<50)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "Please attach base64 string for image upload" };
                    return apiResponse;
                }

                requestObject.userId = tblUserBasic.GetUserId(requestObject.userProfileMobile);
                
                if (requestObject.userId==0)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "Not a valid user" };
                    return apiResponse;
                }

                tblUserBasic.UpdateImage(requestObject);

                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "Image uploaded successfully", data = requestObject };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "Image upload failed, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }

        [Route("api/User/UpdateUserFullName")]
        [HttpPost]
        public CustomResponse UpdateUserFullName(UserUpdateFullName requestObject)
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (string.IsNullOrEmpty(requestObject.FirstName) || requestObject.LastName==null)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "Full Name update failed" };
                    return apiResponse;
                }
                                

                tblUserBasic.UpdateUserFullName(requestObject);

                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "User Full Name Updated Successfully", data = requestObject };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "Profile update missing filedName, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }

        [Route("api/User/GetAll")]
        [HttpGet]
        public CustomResponse GetAllUser()
        {
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {       
                var alluserList= tblUserBasic.GetAllUserEntity();
                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "User retrieved Successfully", data = alluserList };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "User retrieve failed, please try again" };
                return apiResponse;
            }

            return apiResponse;
        }


    }
}
