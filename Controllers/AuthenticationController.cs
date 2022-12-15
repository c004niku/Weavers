using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Weavers.Common.Models.Entities;
using Weavers.Models.Helpers;

namespace Weavers.Controllers
{
    public class AuthenticationController : ApiController
    {


        [Route("api/Otp/Generate")]
        [HttpPost]
        public CustomResponse GenerateOtp(string mobileNumber)
        {
            var response = new CustomResponse { status= HttpStatusCode.OK };
            response.data= MobileMessageGenerate.sendMessage(mobileNumber, "First Test Message", true);
            response.message = "OTP is sent to your Mobile Number";
            return response;
        }

        [Route("api/Otp/Validate")]
        [HttpPost]
        public CustomResponse ValidateOtp(string otp)
        {
            var response = new CustomResponse { status = HttpStatusCode.OK };

            return response;
        }
    }
}
