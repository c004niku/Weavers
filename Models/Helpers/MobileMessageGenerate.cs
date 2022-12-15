using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Weavers.Models.Helpers
{
    public static class MobileMessageGenerate
    {
        public static string sendMessage(string phoneNo, string message, bool generateotp=false)
        {
            string url = "http://api.bulksmsgateway.in/sendmessage.php";
            object otp = null;
            string result = "";

            if (generateotp)
            {
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                otp = _rdm.Next(_min, _max);
                message =string.Format("Dear Customer, Your OTP is {0}. OTP is Valid for 2 Minutes Only. Regards,(Nekaaramitra) KNARAS", otp.ToString());
            }

            message = HttpUtility.UrlPathEncode(message);
            
            String strPost = "?user=" + HttpUtility.UrlPathEncode("nekaaramitra") + "&password=" + HttpUtility.UrlPathEncode("6233725") + "&sender=" + HttpUtility.UrlPathEncode("KNARAS") + "&mobile=" + HttpUtility.UrlPathEncode(phoneNo) + "&type=" + HttpUtility.UrlPathEncode("3") + "&message=" + message + "&template_id=" + HttpUtility.UrlPathEncode("1507165088681981792");
            
            StreamWriter myWriter = null;
            
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url + strPost);
            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader sr.Close();
            }
            return otp.ToString();
        }
    }
}