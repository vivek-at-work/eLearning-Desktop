using System;
using System.Net;
namespace Coneixement.Infrastructure.Helpers.Network
{
    public class WebRequestHelper
    {
        public static String GetResponseString(string baseurl , string poststring)
        {
            string sURL;
            sURL = baseurl + "?" + poststring;
            WebClient webClient = new WebClient();
            string result = webClient.DownloadString(sURL);
            return result.Trim();
        }
    }
}
