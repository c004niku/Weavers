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
    public class UserLoginController : ApiController
    {

        [Route("api/User/Login")]
        [HttpPost]
        public CustomResponse Login(UserLoginEntity requestObject)
        {
            UserEntity savedEntity =null;
            var apiResponse = new CustomResponse { status = HttpStatusCode.OK };
            try
            {
                if (string.IsNullOrEmpty(requestObject.Password))
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "Password must be entered by user" };
                }

                if (string.IsNullOrEmpty(requestObject.UserName))
                {
                    apiResponse = new CustomResponse { status = HttpStatusCode.ExpectationFailed, message = "UserName must be entered by user" };
                }

                savedEntity = tblUserBasic.CheckUserCredential(requestObject);
            }
            catch (Exception ex)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = ex.Message };
            }

            if (savedEntity == null)
            {
                apiResponse = new CustomResponse { status = HttpStatusCode.BadRequest, message = "UserName/Password is invalid" };
            }
            else
            {
                apiResponse.message = "Login sucessfull";
                apiResponse.data = savedEntity;
            }

            

            return apiResponse;
        }


       
    }
}
