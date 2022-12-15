using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Weavers.Common.Models.Entities;
using Weavers.Models.DbHelper;

namespace Weavers.Controllers
{
    public class RegistrationController : ApiController
    {
        [Route("api/Registration/UserBasicSave")]
        [HttpPost]
        public HttpResponseMessage UserBasicSave(UserBasicEntity requestObject)
        {
            UserBasicEntity savedEntity = new UserBasicEntity();
            try
            {
                savedEntity = tblUserBasic.Save(requestObject);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, savedEntity);
        }

        [Route("api/Registration/UserProfessionalSave")]
        [HttpPost]
        public HttpResponseMessage UserProfessionalSave(UserProfessionalDetail requestObject)
        {
            UserProfessionalDetail savedEntity = new UserProfessionalDetail();
            try
            {
                savedEntity = tblUserBasic.SaveUserProfessional(requestObject,"");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, savedEntity);
        }

        [Route("api/Registration/CreateAccount")]
        [HttpPost]
        public HttpResponseMessage CreateAccount(UserCredentialEntity requestObject)
        {
            UserCredentialEntity savedEntity = new UserCredentialEntity();
            try
            {
                if(string.IsNullOrEmpty(requestObject.NewPassword))
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Password must be entered by user");
                }

                savedEntity = tblUserBasic.SaveUserCredential(requestObject);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, savedEntity);
        }

        [Route("api/Registration/UserRegistration")]
        [HttpPost]
        public CustomResponse UserRegistration(UserAccount requestObject)
        {
            UserAccount savedEntity = new UserAccount();
            var apiResponse = new CustomResponse { status=HttpStatusCode.OK };
            try
            {
                if (requestObject.userBasicEntity == null || string.IsNullOrEmpty(requestObject.userBasicEntity.UserName))
                {
                    apiResponse = new CustomResponse { status= HttpStatusCode.ExpectationFailed, message="UserName must be entered by user" };
                    return apiResponse;
                }

                if (requestObject.userCredentialEntity==null || string.IsNullOrEmpty(requestObject.userCredentialEntity.NewPassword))
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "Password must be entered by user" };
                    return apiResponse;
                }

                var existinguser = tblUserBasic.CheckIfUserExisting(requestObject.userBasicEntity);
                if (existinguser)
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.Ambiguous, message = "User with same Mobile/UserName/Email exist" };
                    return apiResponse;
                }

                if(requestObject.userProfessionalDetail.OccupationId==999 
                    && string.IsNullOrEmpty(requestObject.userProfessionalDetail.OtherOccupation))
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.Ambiguous, message = "Please specify Other type" };
                    return apiResponse;
                }

               var userBasicEntity = tblUserBasic.Save(requestObject.userBasicEntity);

                requestObject.userCredentialEntity.ID = userBasicEntity.ID;
                requestObject.userProfessionalDetail.ID = userBasicEntity.ID;

                if (requestObject.userCredentialEntity.ID == 0)
                {
                    Random random = new Random(23432);
                    requestObject.userCredentialEntity.ID = random.Next();
                    requestObject.userProfessionalDetail.ID = requestObject.userCredentialEntity.ID;
                }
                var userCredential = tblUserBasic.SaveUserCredential(requestObject.userCredentialEntity);
                var userProfessionalDetail = tblUserBasic.SaveUserProfessional(requestObject.userProfessionalDetail, requestObject.userBasicEntity.FullName, requestObject.userBasicEntity.MobileNumber);
                apiResponse = new CustomResponse { status = HttpStatusCode.OK, message = "Registered Successfully" };
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = ex.Message+" try again" };
                return apiResponse;
            }
            
            return apiResponse;
        }

        [Route("api/Registration/VerifyUserName")]
        [HttpGet]
        public CustomResponse UserNameAvailable(string userName)
        {
            var customResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                
                var existinguser = tblUserBasic.CheckIfUserExistingByName(userName);
                customResponse.message = existinguser ? "UserName already taken" : "Available";
                if (existinguser)
                    customResponse.status = HttpStatusCode.Found;
            }
            catch (Exception ex)
            {
                customResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = ex.Message };
            }
            return customResponse;
        }

        [Route("api/Registration/VerifyMobile")]
        [HttpGet]
        public CustomResponse MobileAvailable(string mobileNumber)
        {
            var customResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                
                var existinguser = tblUserBasic.CheckIfUserExistingByMobile(mobileNumber);
                customResponse.message = existinguser ? "Mobile Numer associated with other user" : "Available";
                if(existinguser)
                    customResponse.status = HttpStatusCode.Found;
            }
            catch (Exception ex)
            {
                customResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = ex.Message };
            }
            return customResponse;
        }
    }
}
